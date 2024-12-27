using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsCommentsService
{
    PostCommentContent Add(PostComment postCommentModel);
    void Delete(int idPostComment);
    IEnumerable<PostCommentContent> GetAll(int idPost);
}