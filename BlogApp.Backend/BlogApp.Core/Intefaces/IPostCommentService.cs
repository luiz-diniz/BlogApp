using BlogApp.Models;
using BlogApp.Repository.Interfaces;

namespace BlogApp.Core.Intefaces;

public interface IPostCommentService
{
    void Add(PostComment postComment);
    void Delete(int idPostComment);
    IEnumerable<PostComment> GetAll(int idPost);
}