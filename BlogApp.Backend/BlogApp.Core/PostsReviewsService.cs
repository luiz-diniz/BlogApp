using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsReviewsService : IPostsReviewsService
{
    private readonly ILogger<PostsReviewsService> _logger;
    private readonly IPostsReviewsRepository _postReviewRepository;

    public PostsReviewsService(ILogger<PostsReviewsService> logger, IPostsReviewsRepository postReviewRepository)
    {
        _logger = logger;
        _postReviewRepository = postReviewRepository;
    }

    public IEnumerable<PostReviewInfo> GetPostsReviews()
    {
        try
        {
            return _postReviewRepository.GetPostsReviews();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void Update(PostReview postReviewModel)
    {
        try
        { 
            _postReviewRepository.Update(postReviewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}