using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IAuthenticationService
{
    public AuthenticationResult Authenticate(LoginModel loginModel);
}