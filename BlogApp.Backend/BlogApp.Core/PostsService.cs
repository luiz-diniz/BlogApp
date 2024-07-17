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
    private readonly IPostsRepository _postsRepository;
    private readonly IImageService _imageService;
    private readonly IPostsReviewsRepository _postReviewRepository;
    private readonly IConnectionFactory _connectionFactory;
    private readonly IPostsCommentsService _postCommentsService;

    public PostsService(ILogger<PostsService> logger, IPostsRepository postsRepository, IImageService imageService, IPostsReviewsRepository postReviewRepository, IConnectionFactory connectionFactory, IPostsCommentsService postCommentsService)
    {
        _logger = logger;
        _postsRepository = postsRepository;
        _postReviewRepository = postReviewRepository;
        _imageService = imageService;
        _connectionFactory = connectionFactory;
        _postCommentsService = postCommentsService;
    }

    public void Add(Post postModel)
    {
		try
		{
            ReplaceEmptyParagraphs(postModel);

            postModel.PostImageName = _imageService.CreateImage(postModel.PostImageContent, nameof(AppSettingsEnum.PostImageStoragePath));

            using var connection = _connectionFactory.CreateConnection();

            using var transaction = _connectionFactory.CreateTransaction(connection);

            postModel.Id = _postsRepository.Add(postModel, connection, transaction);
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

            var post = _postsRepository.Get(id);

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
            var posts = _postsRepository.GetFeedPosts().ToArray();

            PopulatePostUserImage(posts.ToArray());

            return posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }    
    }

    public IEnumerable<PostFeed> GetUserPosts(int idUser)
    {
        try
        {
            if (idUser <= 0)
                throw new ArgumentOutOfRangeException(nameof(idUser), "Invalid Post Id.");

            var posts = _postsRepository.GetUserPosts(idUser).ToArray();

            PopulatePostUserImage(posts);

            return posts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private void PopulatePostUserImage(IEnumerable<PostFeed> posts)
    {
        foreach (var post in posts)
            post.User.ProfileImageContent = _imageService.GetImage(post.User.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));
    }

    private void ReplaceEmptyParagraphs(Post post)
    {
        post.Content = post.Content.Replace("<p></p>", "<br>");
    }
}