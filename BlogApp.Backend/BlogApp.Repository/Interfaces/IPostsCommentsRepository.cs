using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Repository.Interfaces;

public interface IPostsCommentsRepository
{
    void Add(PostCommentModel postComment);
    void Delete(int idPostComment);
    IEnumerable<PostComment> GetAll(int idPost);
}