using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/posts/likes")]
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
    public IActionResult AddLike([FromBody] PostLikeModel postLike)
    {
        try
        {
            _postLikeService.AddLike(postLike.IdPost, postLike.IdUser);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }

    [HttpDelete]
    public IActionResult RemoveLike([FromBody] PostLikeModel postLike)
    {
        try
        {
            _postLikeService.RemoveLike(postLike.IdPost, postLike.IdUser);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}