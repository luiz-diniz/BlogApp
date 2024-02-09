using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsCommentsService
{
    void Add(PostCommentModel postCommentModel);
    void Delete(int idPostComment);
    IEnumerable<PostComment> GetAll(int idPost);
}