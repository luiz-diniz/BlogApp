using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostCommentService : IPostCommentService
{
    private readonly ILogger<PostCommentService> _logger;
    private readonly IPostCommentRepository _postCommentRepository;

    public PostCommentService(ILogger<PostCommentService> logger, IPostCommentRepository postCommentRepository)
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

    public IEnumerable<PostComment> GetAll(int idPost)
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