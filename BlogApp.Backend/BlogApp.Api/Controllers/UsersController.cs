using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
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
    public IActionResult Add([FromBody] UserModel user)
    {
        try
        {
            _userService.Add(user.ConvertModelToUser());

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}