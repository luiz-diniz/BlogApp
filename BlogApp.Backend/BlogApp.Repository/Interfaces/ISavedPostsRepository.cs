using BlogApp.Models.InputModels;

namespace BlogApp.Repository.Interfaces;

public interface ISavedPostsRepository
{
    void Save(SavedPost savedPostModel);
    void Delete(int idSavedPost);
}