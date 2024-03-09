using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Core.Intefaces;

public interface IAuthenticationService
{
    public AuthenticationResult Authenticate(Login loginModel);
}