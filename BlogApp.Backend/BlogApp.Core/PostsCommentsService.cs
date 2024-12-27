using BlogApp.Core.Enums;
using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsCommentsService : IPostsCommentsService
{
    private readonly ILogger<PostsCommentsService> _logger;
    private readonly IPostsCommentsRepository _postCommentRepository;
    private readonly IImageService _imageService;

    public PostsCommentsService(ILogger<PostsCommentsService> logger, IPostsCommentsRepository postCommentRepository, IImageService imageService)
    {
        _logger = logger;
        _postCommentRepository = postCommentRepository;
        _imageService = imageService;
    }

    public PostCommentContent Add(PostComment postComment)
    {
        try
        {
            var id = _postCommentRepository.Add(postComment);

            var comment = _postCommentRepository.Get(id);

            comment.User.ProfileImageContent = _imageService.GetImage(comment.User.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));

            return comment;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void Delete(int idPostComment)
    {
        try
        {
            _postCommentRepository.Delete(idPostComment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<PostCommentContent> GetAll(int idPost)
    {
        try
        {
            return _postCommentRepository.GetAll(idPost);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}