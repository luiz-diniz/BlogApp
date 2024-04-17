using BlogApp.Core.Enums;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;
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

    public void Add(User userModel)
    {
        try
        {
            if(userModel is null)
                throw new ArgumentNullException(nameof(userModel), "User null.");

            if (_userRepository.VerifyUserExist(userModel.Username))
                throw new UserAlreadyExistsException($"User [{userModel.Username}] already exists.");

            userModel.Password = _passwordManager.GeneratePasswordHash(userModel.Password);
            userModel.ProfileImageName = _imageService.CreateImage(userModel.ProfileImageContent!, nameof(AppSettingsEnum.ProfileImageStoragePath));

            _userRepository.Add(userModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public UserProfile GetUserProfile(string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentOutOfRangeException(nameof(username), "Invalid Username.");

            var userProfile = _userRepository.GetProfileInfo(username);

            if(userProfile is not null)
            {
                userProfile.ProfileImageContent = _imageService.GetImage(userProfile.ProfileImageName, nameof(AppSettingsEnum.ProfileImageStoragePath));

                return userProfile;
            }

            return null!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}