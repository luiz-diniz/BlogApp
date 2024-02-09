using BlogApp.Models;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace BlogApp.Repository.SqlRepository;

public class SavedPostsRepository : ISavedPostsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public SavedPostsRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public void Save(SavedPostModel savedPostModel)
    {
        var query = "INSERT INTO [SavedPosts] (IdPost, IdUser) VALUES (@P0, @P1);";

        var parameters = new object[]
        {
            savedPostModel.IdPost,
            savedPostModel.IdUser
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }

    public void Delete(int idSavedPost)
    {
        var query = "DELETE FROM [SavedPosts] WHERE Id = @P0";

        var parameters = new object[]
        {
            idSavedPost
        };

        _queryExecutor.ExecuteNonQuery(query, parameters);
    }
}