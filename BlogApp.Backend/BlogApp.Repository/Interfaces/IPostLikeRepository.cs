using BlogApp.Models;

namespace BlogApp.Repository.Interfaces;

public interface IPostLikeRepository
{
    void AddLike(int idPost, int idUser);
    void RemoveLike(int idPost, int idUser);
    bool VerifyPostLiked(int idPost, int idUser);
}