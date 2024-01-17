using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

public class ApiControllerBase : ControllerBase
{
    protected IActionResult ReturnError(int statusCode, Exception exception, ILogger logger)
    {
        logger.LogError(exception, exception.Message);
        return StatusCode(statusCode, exception.Message);
    }
}