using BlogApp.Core.Enums;
using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace BlogApp.Core;

public class PostService : IPostService
{
    private readonly ILogger<PostService> _logger;
    private readonly IPostRepository _postRepository;
    private readonly IImageService _imageService;
    private readonly IPostReviewRepository _postReviewRepository;
    private readonly IConnectionFactory _connectionFactory;

    public PostService(ILogger<PostService> logger, IPostRepository postRepository, IImageService imageService, IPostReviewRepository postReviewRepository, IConnectionFactory connectionFactory)
    {
        _logger = logger;
        _postRepository = postRepository;
        _postReviewRepository = postReviewRepository;
        _imageService = imageService;
        _connectionFactory = connectionFactory;
    }
        
    public void Add(Post post)
    {
		try
		{           
            post.PostImageName = _imageService.CreateImage(post.PostImageContent!, nameof(AppSettingsEnum.PostImageStoragePath));

            using var connection = _connectionFactory.CreateConnection();

            using var tx = _connectionFactory.CreateTransaction(connection);

            post.Id = _postRepository.Add(post, connection, tx);
            _postReviewRepository.Add(post, connection, tx);

            tx.Commit();
		}
		catch (Exception ex)
		{
            _logger.LogError(ex, ex.Message);
			throw;
		}
    }

    public IEnumerable<Post> GetAll()
    {
        try
        {
            var posts = _postRepository.GetAll();

            if (posts is null)
                return null!;

            foreach(var post in posts)       
                post.PostImageContent = _imageService.GetImage(post.PostImageName, nameof(AppSettingsEnum.PostImageStoragePath));

            return posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }    
    }
}