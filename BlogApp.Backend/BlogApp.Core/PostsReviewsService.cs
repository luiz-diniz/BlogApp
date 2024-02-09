using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Models.Enums;
using BlogApp.Models.InputModels;
using BlogApp.Repository;
using BlogApp.Repository.Interfaces;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsReviewsService : IPostsReviewsService
{
    private readonly ILogger<PostsReviewsService> _logger;
    private readonly IPostsReviewsRepository _postReviewRepository;
    private readonly IPostsRepository _postsRepository;
    private readonly IConnectionFactory _connectionFactory;

    public PostsReviewsService(ILogger<PostsReviewsService> logger, IPostsReviewsRepository postReviewRepository, IPostsRepository postsRepository, IConnectionFactory connectionFactory)
    {
        _logger = logger;
        _postReviewRepository = postReviewRepository;
        _postsRepository = postsRepository;
        _connectionFactory = connectionFactory;
    }

    public void Update(PostReviewModel postReviewModel)
    {
        try
        { 
            if(postReviewModel.Status == StatusEnum.Approved)
            {
                using var connection = _connectionFactory.CreateConnection();
                using var transaction = _connectionFactory.CreateTransaction(connection);

                _postReviewRepository.Publish(postReviewModel, connection, transaction);
                _postsRepository.Publish(postReviewModel.IdPost, connection, transaction);

                transaction.Commit();
            }
            else
                _postReviewRepository.Update(postReviewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}