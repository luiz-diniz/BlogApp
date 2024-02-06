using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IAuthenticationService
{
    public AuthenticationResult Authenticate(string username, string password);
}