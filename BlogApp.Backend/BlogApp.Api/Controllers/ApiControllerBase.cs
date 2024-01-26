using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogApp.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected string SerializeReturn(object model)
    {
        return JsonConvert.SerializeObject(model, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }

    protected IActionResult ReturnError(int statusCode, Exception exception, ILogger logger)
    {
        logger.LogError(exception, exception.Message);
        return StatusCode(statusCode, exception.Message);
    }
}