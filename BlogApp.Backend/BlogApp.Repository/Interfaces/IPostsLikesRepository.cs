using BlogApp.Models;
using BlogApp.Models.InputModels;

namespace BlogApp.Repository.Interfaces;

public interface IPostsLikesRepository
{
    void AddLike(PostLike postLikeModel);
    void RemoveLike(PostLike postLikeModel);
    bool VerifyPostLiked(PostLike postLikeModel);
}