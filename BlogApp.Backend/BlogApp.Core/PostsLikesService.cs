using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsLikesService : IPostsLikesService
{
    private readonly ILogger<PostsLikesService> _logger;
    private readonly IPostsLikesRepository _postLikeRepository;

    public PostsLikesService(ILogger<PostsLikesService> logger, IPostsLikesRepository postLikeRepository)
    {
        _logger = logger;
        _postLikeRepository = postLikeRepository;
    }

    public void AddLike(PostLike postLikeModel)
    {
        try
        {
            if (_postLikeRepository.VerifyPostLiked(postLikeModel))
                return;

            _postLikeRepository.AddLike(postLikeModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void RemoveLike(PostLike postLikeModel)
    {
        try
        {
            _postLikeRepository.RemoveLike(postLikeModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}