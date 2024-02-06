using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[Route("api/v1/[controller]")]
[AllowAnonymous]
public class AuthenticationController : ApiControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
	{
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public IActionResult Authenticate(LoginModel loginModel)
    {
		try
		{
            var result = _authenticationService.Authenticate(loginModel.Username, loginModel.Password);

            return Ok(result);
        }
		catch (Exception ex)
		{
            return ReturnError(500, ex, _logger);	
		}
    }
}