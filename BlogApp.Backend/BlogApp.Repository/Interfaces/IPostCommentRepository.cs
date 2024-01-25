using BlogApp.Models;

namespace BlogApp.Repository.Interfaces;

public interface IPostCommentRepository
{
    void Add(PostComment postComment);
    void Delete(int idPostComment);
    IEnumerable<PostComment> GetAll(int idPost);
}