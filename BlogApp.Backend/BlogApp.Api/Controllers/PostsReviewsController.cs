using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/Posts")]
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
    [Route("Reviews")]
    public IActionResult GetPostsReviews()
    {
        try
        {
            var posts = _postReviewService.GetPostsReviews();

            return Ok(SerializeReturn(posts));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, _logger);
        }
    }

    [HttpGet]
    [Route("{idPost}/Reviews")]
    public IActionResult GetPostForReview(int idPost)
    {
        try
        {
            var post = _postReviewService.GetPostForReview(idPost);

            return Ok(SerializeReturn(post));
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, _logger);
        }
    }

    [HttpPut]
    [Route("Reviews")]
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