using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IPostsCommentsService
{
    void Add(PostComment postComment);
    void Delete(int idPostComment);
    IEnumerable<PostComment> GetAll(int idPost);
}