using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Repository.Interfaces;

public interface IPostsLikesRepository
{
    void AddLike(PostLikeModel postLikeModel);
    void RemoveLike(PostLikeModel postLikeModel);
    bool VerifyPostLiked(PostLikeModel postLikeModel);
}