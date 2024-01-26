using BlogApp.Api.Extensions.Converters;
using BlogApp.Api.Models;
using BlogApp.Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[Route("api/v1/posts")]
public class PostsCommentsController : ApiControllerBase
{
    private readonly ILogger<PostsCommentsController> _logger;
    private readonly IPostsCommentsService _postCommentService;

    public PostsCommentsController(ILogger<PostsCommentsController> logger, IPostsCommentsService postCommentService)
    {
        _logger = logger;
        _postCommentService = postCommentService;
    }

    [HttpPost]
    [Route("comments")]
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
    [Route("comments/{idPostComment}")]
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
    [Route("{idPost}/comments")]
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