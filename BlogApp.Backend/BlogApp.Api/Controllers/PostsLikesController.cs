using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/Posts/Likes")]
public class PostsLikesController : ApiControllerBase
{
    private readonly ILogger<PostsLikesController> _logger;
    private readonly IPostsLikesService _postLikeService;

    public PostsLikesController(ILogger<PostsLikesController> logger, IPostsLikesService postLikeService)
    {
        _logger = logger;
        _postLikeService = postLikeService;
    }

    [HttpPost]
    public IActionResult AddLike([FromBody] PostLike postLikeModel)
    {
        try
        {
            _postLikeService.AddLike(postLikeModel);

            return Ok();
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, _logger);
        }
    }

    [HttpDelete]
    public IActionResult RemoveLike([FromBody] PostLike postLikeModel)
    {
        try
        {
            _postLikeService.RemoveLike(postLikeModel);

            return Ok();
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, _logger);
        }
    }
}