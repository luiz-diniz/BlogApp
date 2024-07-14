using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/Posts/Reviews")]
[Authorize(Policy = PolicyConstants.MustBeAdmin)]
public class PostsReviewsController : ApiControllerBase
{
    private readonly ILogger<PostsReviewsController> _logger;
    private readonly IPostsReviewsService _postReviewService;

    public PostsReviewsController(ILogger<PostsReviewsController> logger, IPostsReviewsService postReviewService)
    {
        _logger = logger;
        _postReviewService = postReviewService;
    }

    [HttpGet]
    public IActionResult GetReviewPosts()
    {
        try
        {
            var posts = _postReviewService.GetReviewPosts();

            return Ok(SerializeReturn(posts));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, _logger);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] PostReview postReviewModel)
    {
        try
        {
            _postReviewService.Update(postReviewModel);

            return Ok();
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, _logger);
        }
    }
}