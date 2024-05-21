using BlogApp.Repository.Interfaces;

namespace BlogApp.Repository.SqlRepository;

public class PostsCategoriesRepository : IPostsCategoriesRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PostsCategoriesRepository(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public Dictionary<int, string> GetCategories()
    {
        var query = "SELECT * FROM [PostsCategories]";

        using var reader = _queryExecutor.ExecuteReader(query);

        var categories = new Dictionary<int, string>();

        while (reader.Read())
            categories.Add(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]));

        return categories;
    }
}