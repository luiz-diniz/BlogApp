using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostCommentRepository : IPostCommentRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PostCommentRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Add(PostComment postComment)
    {
        var query = $"INSERT INTO [PostComments] (IdPost, IdUser, Comment) VALUES (@P0, @P1, @P2);";
        
        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        var parameters = new object[]
        {
            postComment.Id,
            postComment.User.Id,
            postComment.Comment
        };

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void Delete(int idPostComment)
    {
        var query = $"DELETE FROM [PostComments] WHERE Id = @P0";

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
        var query = $@"SELECT P.*, U.Username FROM [PostComments] P 
                        INNER JOIN [User] U ON P.IdUser = U.Id 
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