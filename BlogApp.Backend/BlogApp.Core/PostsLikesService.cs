using BlogApp.Core.Intefaces;
using BlogApp.Models;
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

    public void AddLike(int idPost, int idUser)
    {
        try
        {
            if (_postLikeRepository.VerifyPostLiked(idPost, idUser))
                return;

            _postLikeRepository.AddLike(idPost, idUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void RemoveLike(int idPost, int idUser)
    {
        try
        {
            _postLikeRepository.RemoveLike(idPost, idUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}