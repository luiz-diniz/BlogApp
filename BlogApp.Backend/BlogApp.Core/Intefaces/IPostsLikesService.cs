using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IPostsLikesService
{
    void AddLike(PostLikeModel postLikeModel);
    void RemoveLike(PostLikeModel postLikeModel);
}