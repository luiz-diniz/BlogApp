using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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

    protected IActionResult ReturnError(HttpStatusCode statusCode, Exception exception, ILogger logger = null)
    {
        if(logger != null)
            logger.LogError(exception, exception.Message);

        return StatusCode((int)statusCode, exception.Message);
    }
}