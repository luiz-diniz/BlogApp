namespace BlogApp.Repository.Interfaces;

public interface IPostsCategoriesRepository
{
    Dictionary<int, string> GetCategories();
}