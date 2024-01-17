using BlogApp.Core.Enums;
using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class PostService : IPostService
{
    private readonly ILogger<PostService> _logger;
    private readonly IPostRepository _postRepository;
    private readonly IImageService _imageService;

    public PostService(ILogger<PostService> logger, IPostRepository postRepository, IImageService imageService)
    {
        _logger = logger;
        _postRepository = postRepository;
        _imageService = imageService;
    }
        
    public void Add(Post post)
    {
		try
		{
            post.PostImageName = _imageService.CreateImage(post.PostImageContent!, nameof(AppSettingsEnum.PostImageStoragePath));

            _postRepository.Add(post);
		}
		catch (Exception ex)
		{
            _logger.LogError(ex, ex.Message);
			throw;
		}
    }
}