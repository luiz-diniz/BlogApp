using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;

namespace BlogApp.Repository.SqlRepository;

public class PostsCommentsRepository : IPostsCommentsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsCommentsRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void Add(PostComment postComment)
    {
        var query = $"INSERT INTO [PostsComments] (IdPost, IdUser, Comment) VALUES (@P0, @P1, @P2);";   

        var parameters = new object[]
        {
            postComment.IdPost,
            postComment.IdUser,
            postComment.Comment
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public void Delete(int idPostComment)
    {
        var query = $"DELETE FROM [PostsComments] WHERE Id = @P0";

        var parameters = new object[]
        {
            idPostComment
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public IEnumerable<PostCommentContent> GetAll(int idPost)
    {
        var query = $@"SELECT P.*, U.Username, U.ProfileImageName FROM [PostsComments] P 
                        INNER JOIN [Users] U ON P.IdUser = U.Id 
                        WHERE P.IdPost = @P0";
              
        var parameters = new object[]
        {
            idPost
        };

        using var reader = _queryExecutor.ExecuteReader(query, parameters);

        var comments = new List<PostCommentContent>();

        while(reader.Read())
        {
            comments.Add(new PostCommentContent
            {
                Id = Convert.ToInt32(reader["Id"]),
                User = new UserProfile
                {
                    Id = Convert.ToInt32(reader["IdUser"]),
                    Username = Convert.ToString(reader["Username"]),
                    ProfileImageName = Convert.ToString(reader["ProfileImageName"])
                },
                Comment = Convert.ToString(reader["Comment"]),
                CreationDate = Convert.ToDateTime(reader["CreationDate"])                
            });
        }

        if (comments.Count > 0)
            return comments;

        return null!;
    }
}