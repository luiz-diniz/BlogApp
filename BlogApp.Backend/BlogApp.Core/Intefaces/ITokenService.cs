using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface ITokenService
{
    string GetToken(UserCredentialsModel user);
}