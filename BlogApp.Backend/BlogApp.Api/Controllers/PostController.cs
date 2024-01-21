using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogApp.Api.Controllers;

public class PostController : ApiControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostService _postService;

    public PostController(ILogger<PostController> logger, IPostService postService)
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
            return ReturnError(500, ex, _logger);
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
            return ReturnError(500, ex, _logger);
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
            return ReturnError(500, ex, _logger);
        }
    }
}