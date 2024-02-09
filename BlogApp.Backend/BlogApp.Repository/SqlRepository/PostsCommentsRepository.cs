using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostsCommentsRepository : IPostsCommentsRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostsCommentsRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Add(PostCommentModel postComment)
    {
        var query = $"INSERT INTO [PostsComments] (IdPost, IdUser, Comment) VALUES (@P0, @P1, @P2);";
        
        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            postComment.IdPost,
            postComment.IdUser,
            postComment.Comment
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void Delete(int idPostComment)
    {
        var query = $"DELETE FROM [PostsComments] WHERE Id = @P0";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            idPostComment
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public IEnumerable<PostComment> GetAll(int idPost)
    {
        var query = $@"SELECT P.*, U.Username FROM [PostsComments] P 
                        INNER JOIN [Users] U ON P.IdUser = U.Id 
                        WHERE P.IdPost = @P0";

        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        cmd.CommandType = CommandType.Text;

        var parameters = new object[]
        {
            idPost
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        var reader = cmd.ExecuteReader();

        var comments = new List<PostComment>();

        while(reader.Read())
        {
            comments.Add(new PostComment
            {
                Id = Convert.ToInt32(reader["Id"]),
                User = new User
                {
                    Id = Convert.ToInt32(reader["IdUser"]),
                    Username = Convert.ToString(reader["Username"])
                },
                Comment = Convert.ToString(reader["Comment"]),
                CommentDate = Convert.ToDateTime(reader["CommentDate"])                
            });
        }

        if (comments.Count > 0)
            return comments;

        return null!;
    }
}