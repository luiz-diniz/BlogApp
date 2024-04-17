using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface ITokenService
{
    string GetToken(UserCredentials user);
}