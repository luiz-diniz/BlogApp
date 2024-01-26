using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using log4net.Core;
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

    public void Update(PostReview postReview)
    {
        try
        {
            _postReviewRepository.Update(postReview);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}