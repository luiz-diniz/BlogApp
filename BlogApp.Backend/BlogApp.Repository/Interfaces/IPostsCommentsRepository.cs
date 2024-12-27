using BlogApp.Models.InputModels;
using BlogApp.Models.OutputModels;

namespace BlogApp.Repository.Interfaces;

public interface IPostsCommentsRepository
{
    int Add(PostComment postComment);
    void Delete(int idPostComment);
    IEnumerable<PostCommentContent> GetAll(int idPost);
    PostCommentContent Get(int idComment);
}