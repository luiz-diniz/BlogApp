using BlogApp.Api.Extensions;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] UserModel user)
    {
        try
        {
            _userService.Add(user.ConvertModelToUser());

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}