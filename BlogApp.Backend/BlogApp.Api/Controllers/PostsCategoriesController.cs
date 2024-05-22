using System.Net;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[Route("api/v1/Posts/Categories")]
public class PostsCategoriesController : ApiControllerBase
{
    private readonly ILogger<PostsCategoriesController> _logger;
    private readonly IPostsCategoriesService _postsCategoriesService;

    public PostsCategoriesController(ILogger<PostsCategoriesController> logger, IPostsCategoriesService postsCategoriesService)
    {
        _logger = logger;
        _postsCategoriesService = postsCategoriesService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        try
        {
            var categories = _postsCategoriesService.GetCategories();

            if (!categories.Any())
                return NotFound();

            return Ok(SerializeReturn(categories));
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}