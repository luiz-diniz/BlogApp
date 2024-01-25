using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

public class PostCommentController : ApiControllerBase
{
    private readonly ILogger<PostCommentController> _logger;
    private readonly IPostCommentService _postCommentService;

    public PostCommentController(ILogger<PostCommentController> logger, IPostCommentService postCommentService)
    {
        _logger = logger;
        _postCommentService = postCommentService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] PostCommentModel postComment)
    {
        try
        {
            _postCommentService.Add(postComment.ConvertToPostComment());

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(500, ex, _logger);
        }
    }

    [HttpDelete]
    [Route("{idPostComment}")]
    public IActionResult Delete(int idPostComment)
    {
        try
        {
            _postCommentService.Delete(idPostComment);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(500, ex, _logger);
        }
    }

    [HttpGet]
    [Route("{idPost}")]
    public IActionResult GetAll(int idPost)
    {
        try
        {
            var posts = _postCommentService.GetAll(idPost);

            var postsJson = SerializeReturn(posts);

            return Ok(postsJson);
        }
        catch (Exception ex)
        {
            return ReturnError(500, ex, _logger);
        }
    }
}