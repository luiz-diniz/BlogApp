using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Repository.Interfaces;

public interface IPostsCommentsRepository
{
    void Add(PostComment postComment);
    void Delete(int idPostComment);
    IEnumerable<PostCommentContent> GetAll(int idPost);
}