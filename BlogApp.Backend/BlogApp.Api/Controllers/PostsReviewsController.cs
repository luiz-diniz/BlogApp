using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[Route("api/v1/posts/reviews")]
public class PostsReviewsController : ApiControllerBase
{
    private readonly ILogger<PostsReviewsController> _logger;
    private readonly IPostsReviewsService _postReviewService;

    public PostsReviewsController(ILogger<PostsReviewsController> logger, IPostsReviewsService postReviewService)
    {
        _logger = logger;
        _postReviewService = postReviewService;
    }

    [HttpPut]
    public IActionResult Update([FromBody] PostReviewModel post)
    {
        try
        {
            _postReviewService.Update(post.ConvertModelToPostReview());

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(500, ex, _logger);
        }
    }
}