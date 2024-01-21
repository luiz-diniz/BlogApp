using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IPostLikeService
{
    void AddLike(int idPost, int idUser);
    void RemoveLike(int idPost, int idUser);
}