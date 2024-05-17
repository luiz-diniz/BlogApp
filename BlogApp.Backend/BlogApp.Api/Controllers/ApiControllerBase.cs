using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace BlogApp.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected string SerializeReturn(object model)
    {
        return JsonConvert.SerializeObject(model, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
    }

    protected IActionResult ReturnError(HttpStatusCode statusCode, Exception exception, ILogger logger)
    {
        logger.LogError(exception, exception.Message);

        return StatusCode((int)statusCode, exception.Message);
    }

    protected IActionResult ReturnError(HttpStatusCode statusCode, Exception exception, string message, ILogger logger)
    {
        logger.LogError(exception, exception.Message);

        return StatusCode((int)statusCode, message);
    }
}