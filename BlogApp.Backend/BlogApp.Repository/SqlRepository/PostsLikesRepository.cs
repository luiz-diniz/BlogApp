using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class PostsLikesRepository : IPostsLikesRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsLikesRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void AddLike(PostLikeModel postLikeModel)
    {
        var query = "INSERT INTO [PostsLikes] (IdPost, IdUser) VALUES (@P0, @P1);";

        var parameters = new object[]
        {
            postLikeModel.IdPost,
            postLikeModel.IdUser
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public void RemoveLike(PostLikeModel postLikeModel)
    {
        var query = "DELETE FROM [PostsLikes] WHERE IdPost = @P0 AND IdUser = @P1;";

        var parameters = new object[]
        {
            postLikeModel.IdPost,
            postLikeModel.IdUser
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public bool VerifyPostLiked(PostLikeModel postLikeModel)
    {
        var query = "SELECT COUNT(*) AS COUNT FROM [PostsLikes] WHERE IdPost = @P0 AND IdUser = @P1;";
       
        var parameters = new object[]
        {
            postLikeModel.IdPost,
            postLikeModel.IdUser
        };
        
        using var reader = _queryExecutor.ExecuteReader(query, parameters);
        
        if(reader.Read())
          return Convert.ToInt32(reader["COUNT"]) == 1;

        return false;
    }
}