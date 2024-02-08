using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/[controller]")]
public class PostsController : ApiControllerBase
{
    private readonly ILogger<PostsController> _logger;
    private readonly IPostsService _postService;

    public PostsController(ILogger<PostsController> logger, IPostsService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] PostModel post)
    {
        try
        {
            _postService.Add(post.ConvertModelToPost());

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var post = _postService.Get(id);

            var postJson = SerializeReturn(post);

            return Ok(postJson);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var posts = _postService.GetAll();

            var jsonPosts = SerializeReturn(posts);

            return Ok(jsonPosts);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}