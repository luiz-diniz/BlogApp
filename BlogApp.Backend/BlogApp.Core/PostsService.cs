using BlogApp.Core.Enums;
using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostsService : IPostsService
{
    private readonly ILogger<PostsService> _logger;
    private readonly IPostsRepository _postRepository;
    private readonly IImageService _imageService;
    private readonly IPostsReviewsRepository _postReviewRepository;
    private readonly IConnectionFactory _connectionFactory;
    private readonly IPostsCommentsService _postCommentsService;
    private readonly IPostsLikesService _postLikesService;

    public PostsService(ILogger<PostsService> logger, IPostsRepository postRepository, IImageService imageService, IPostsReviewsRepository postReviewRepository, IConnectionFactory connectionFactory, IPostsCommentsService postCommentsService, IPostsLikesService postLikesService)
    {
        _logger = logger;
        _postRepository = postRepository;
        _postReviewRepository = postReviewRepository;
        _imageService = imageService;
        _connectionFactory = connectionFactory;
        _postCommentsService = postCommentsService;
        _postLikesService = postLikesService;
    }

    public void Add(Post postModel)
    {
		try
		{
            postModel.PostImageName = _imageService.CreateImage(postModel.PostImageContent, nameof(AppSettingsEnum.PostImageStoragePath));

            using var connection = _connectionFactory.CreateConnection();

            using var transaction = _connectionFactory.CreateTransaction(connection);

            postModel.Id = _postRepository.Add(postModel, connection, transaction);
            _postReviewRepository.Add(postModel, connection, transaction);

            transaction.Commit();
		}
		catch (Exception ex)
		{
            _logger.LogError(ex, ex.Message);
			throw;
		}
    }

    public PostInfo Get(int id)
    {
        try
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid Post Id.");

            var post = _postRepository.Get(id);

            if(post is not null)
            {
                post.PostImageContent = _imageService.GetImage(post.PostImageName, nameof(AppSettingsEnum.PostImageStoragePath));
                post.User.ProfileImageContent = _imageService.GetImage(post.User.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));

                post.Comments = _postCommentsService.GetAll(id);

                if(post.Comments is not null)
                    foreach (var comment in post.Comments)              
                        comment.User.ProfileImageContent = _imageService.GetImage(comment.User.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));
                
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

    public IEnumerable<PostFeed> GetFeedPosts()
    {
        try
        {
            var posts = _postRepository.GetFeedPosts();

            if (posts is null)
                return null!;

            foreach (var post in posts)          
                post.User.ProfileImageContent = _imageService.GetImage(post.User.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));           

            return posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }    
    }
}