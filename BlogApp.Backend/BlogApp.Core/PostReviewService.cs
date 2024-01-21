using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostReviewService : IPostReviewService
{
    private readonly ILogger<PostReviewService> _logger;
    private readonly IPostReviewRepository _postReviewRepository;

    public PostReviewService(ILogger<PostReviewService> logger, IPostReviewRepository postReviewRepository)
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