using BlogApp.Models.OutputModels;

namespace BlogApp.Repository.Interfaces;

public interface IPostsCategoriesRepository
{
    IEnumerable<PostCategory> GetCategories();
}