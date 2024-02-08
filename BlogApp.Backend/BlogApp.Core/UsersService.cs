using BlogApp.Core.Enums;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class UsersService : IUsersService
{
    private readonly ILogger<UsersService> _logger;
    private readonly IUsersRepository _userRepository;
    private readonly IPasswordService _passwordManager;
    private readonly IImageService _imageService;

    public UsersService(ILogger<UsersService> logger, IUsersRepository userRepository, IPasswordService passwordManager, IImageService imageService)
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

            if (_userRepository.VerifyUserExist(user.Username))
                throw new UserAlreadyExistsException($"User [{user.Username}] already exists.");

            user.Password = _passwordManager.GeneratePasswordHash(user.Password);
            user.ProfileImageName = _imageService.CreateImage(user.ProfileImageContent!, nameof(AppSettingsEnum.ProfileImageStoragePath));

            _userRepository.Add(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}