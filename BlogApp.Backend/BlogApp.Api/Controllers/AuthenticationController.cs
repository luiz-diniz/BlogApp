using BlogApp.Api.Models;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

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
        catch (InvalidUserCredentialsException ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid Username or Password.");
        }
        catch (Exception ex)
		{
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);	
		}
    }
}