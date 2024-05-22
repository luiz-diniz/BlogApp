using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;

namespace BlogApp.Repository.SqlRepository;

public class PostsCategoriesRepository : IPostsCategoriesRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsCategoriesRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public IEnumerable<PostCategory> GetCategories()
    {
        var query = "SELECT * FROM [PostsCategories]";

        using var reader = _queryExecutor.ExecuteReader(query);

        var categories = new List<PostCategory>();

        while (reader.Read())
            categories.Add(new PostCategory
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"])
            });

        return categories;
    }
}