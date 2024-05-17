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

    public void Add(User user)
    {
        try
        {
            ValidateUserInput(user);

            user.Password = _passwordManager.GeneratePasswordHash(user.Password);
            user.IdRole = 2;

            AssignProfileImage(user);

            _userRepository.Add(user);
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

    private void AssignProfileImage(User user)
    {
        if (string.IsNullOrWhiteSpace(user.ProfileImageContent))
            user.ProfileImageName = _imageService.CreateImage($"data:image/jpg;base64,{Convert.ToBase64String(Properties.Resources.DefaultProfilePicture)}", nameof(AppSettingsEnum.ProfileImageStoragePath));
        else
            user.ProfileImageName = _imageService.CreateImage(user.ProfileImageContent, nameof(AppSettingsEnum.PostImageStoragePath));
    }

    private void ValidateUserInput(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user), "User null.");

        if (!string.Equals(user.Password, user.PasswordConfirmation, StringComparison.Ordinal))
            throw new InvalidPasswordConfirmationException("Passwords don't match");

        if (_userRepository.VerifyUserExist(user.Username))
            throw new UserAlreadyExistsException($"User [{user.Username}] already taken");

        if (_userRepository.VerifyEmailExist(user.Email))
            throw new EmailAlreadyExistsException($"Email [{user.Email}] already taken");

    }
}