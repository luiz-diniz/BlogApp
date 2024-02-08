using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
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
            return ReturnError(HttpStatusCode.InternalServerError, ex, _logger);
        }
    }
}