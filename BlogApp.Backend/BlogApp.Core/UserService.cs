using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordManager;
    private readonly IImageService _imageService;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository, IPasswordService passwordManager, IImageService imageService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _imageService = imageService;
    }

    public void Add(User user)
    {
        try
        {
            if(user is null)
                throw new ArgumentNullException(nameof(user), "User null.");

            user.Password = _passwordManager.GeneratePasswordHash(user.Password);
            user.ProfilePictureName = _imageService.CreateImage(user.ProfilePictureContent!);

            _userRepository.Add(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}