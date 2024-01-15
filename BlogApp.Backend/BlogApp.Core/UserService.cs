using BlogApp.Core.Intefaces;
using BlogApp.Models;
using BlogApp.Repository.Interfaces;

namespace BlogApp.Core;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Add(User user)
    {
        try
        {
            _userRepository.Add(user);
        }
        catch (Exception)
        {
            throw;
        }
    }
}