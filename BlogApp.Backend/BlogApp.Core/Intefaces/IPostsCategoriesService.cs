namespace BlogApp.Core.Intefaces;

public interface IPostsCategoriesService
{
    Dictionary<int, string> GetCategories();
}