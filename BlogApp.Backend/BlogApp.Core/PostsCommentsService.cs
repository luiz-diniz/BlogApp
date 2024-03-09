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

    public PostsCommentsService(ILogger<PostsCommentsService> logger, IPostsCommentsRepository postCommentRepository)
    {
        _logger = logger;
        _postCommentRepository = postCommentRepository;
    }

    public void Add(PostComment postComment)
    {
        try
        {
            _postCommentRepository.Add(postComment);
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