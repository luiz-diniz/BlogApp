namespace BlogApp.Repository.Interfaces;

public interface ISavedPostsRepository
{
    void Save(int idPost, int idUser);
    void Delete(int idSavedPost);
}