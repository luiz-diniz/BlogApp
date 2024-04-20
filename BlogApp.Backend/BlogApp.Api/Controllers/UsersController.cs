using BlogApp.Core.Exceptions;
using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _userService;

    public UsersController(ILogger<UsersController> logger, IUsersService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Add([FromBody] User userModel)
    {
        try
        {
            _userService.Add(userModel);

            return Ok();
        }
        catch(UserAlreadyExistsException ex)
        {
            return ReturnError(HttpStatusCode.BadRequest, ex, _logger);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }

    [HttpGet("{username}")]
    [AllowAnonymous]
    public IActionResult GetUserProfile(string username)
    {
        try
        {
            var userProfile = _userService.GetUserProfile(username);

            return Ok(SerializeReturn(userProfile));
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}