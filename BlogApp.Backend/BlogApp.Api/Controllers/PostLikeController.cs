using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

public class PostLikeController : ApiControllerBase
{
    private readonly ILogger<PostLikeController> _logger;
    private readonly IPostLikeService _postLikeService;

    public PostLikeController(ILogger<PostLikeController> logger, IPostLikeService postLikeService)
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
            return ReturnError(500, ex, _logger);            
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
            return ReturnError(500, ex, _logger);
        }
    }
}