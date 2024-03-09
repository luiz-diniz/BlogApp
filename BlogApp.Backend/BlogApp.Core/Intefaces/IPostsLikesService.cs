using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsLikesService
{
    void AddLike(PostLike postLikeModel);
    void RemoveLike(PostLike postLikeModel);
}