using BlogApp.Models.OutputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsCategoriesService
{
    IEnumerable<PostCategory> GetCategories();
}