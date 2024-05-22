using BlogApp.Core.Intefaces;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsCategoriesService : IPostsCategoriesService
{
    private readonly ILogger _logger;
    private readonly IPostsCategoriesRepository _postsCategoriesRepository;

    public PostsCategoriesService(ILogger<PostsCategoriesService> logger, IPostsCategoriesRepository postsCategoriesRepository)
    {
        _logger = logger;
        _postsCategoriesRepository = postsCategoriesRepository;
    }

    public IEnumerable<PostCategory> GetCategories()
    {
		try
        {
            return _postsCategoriesRepository.GetCategories();
        }
		catch (Exception ex)
		{
            _logger.LogError(ex, ex.Message);
			throw;
		}
    }
}