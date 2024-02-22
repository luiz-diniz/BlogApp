using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using BlogApp.Models.Enums;
using BlogApp.Models.InputModels;

namespace BlogApp.Repository.SqlRepository;

public class PostsReviewsRepository : IPostsReviewsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsReviewsRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void Add(PostModel postModel, IDbConnection connection, IDbTransaction transaction)
    {
        var query = "INSERT INTO [PostsReviews] (IdPost, Status) VALUES (@P0, @P1);";

        var parameters = new object[]
        {
            postModel.Id,
            (int)StatusEnum.Pending
        };

        _queryExecutor.ExecuteNonQuery(connection, transaction, query, parameters);
    }

    public void Update(PostReviewModel postReviewModel)
    {
        var query = "UPDATE [PostsReviews] SET [IdUserReviewer] = @P0, [Status] = @P1, [Feedback] = @P2, [ReviewDate] = @P3 WHERE [IdPost] = @P4;";

        var parameters = new object[]
        {
            postReviewModel.IdUserReviewer,
            (int)postReviewModel.Status,
            postReviewModel.Feedback,
            DateTime.Now,
            postReviewModel.IdPost
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }
}