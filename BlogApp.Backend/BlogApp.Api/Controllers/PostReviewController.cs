using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

public class PostReviewController : ApiControllerBase
{
    private readonly ILogger<PostReviewController> _logger;
    private readonly IPostReviewService _postReviewService;

    public PostReviewController(ILogger<PostReviewController> logger, IPostReviewService postReviewService)
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