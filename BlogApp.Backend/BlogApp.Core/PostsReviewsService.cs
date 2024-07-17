using BlogApp.Core.Enums;
using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using BlogApp.Repository.SqlRepository;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsReviewsService : IPostsReviewsService
{
    private readonly ILogger<PostsReviewsService> _logger;
    private readonly IPostsReviewsRepository _postReviewRepository;
    private readonly IImageService _imageService;

    public PostsReviewsService(ILogger<PostsReviewsService> logger, IPostsReviewsRepository postReviewRepository, IImageService imageService)
    {
        _logger = logger;
        _postReviewRepository = postReviewRepository;
        _imageService = imageService;
    }

    public PostReviewCompleteInfo GetPostForReview(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid Post Id.");

            var post = _postReviewRepository.GetPostForReview(id);

            if (post is not null)
            {
                post.PostImageContent = _imageService.GetImage(post.PostImageName, nameof(AppSettingsEnum.PostImageStoragePath));
                post.User.ProfileImageContent = _imageService.GetImage(post.User.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));

                return post;
            }

            return null!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
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