namespace BlogApp.Core.Intefaces;

public interface ISavedPostsService
{
    void Save(int idPost, int idUser);
    void Delete(int idSavedPost);
}