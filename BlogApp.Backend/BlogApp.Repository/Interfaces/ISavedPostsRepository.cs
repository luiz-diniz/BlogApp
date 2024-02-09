using BlogApp.Models.InputModels;

namespace BlogApp.Repository.Interfaces;

public interface ISavedPostsRepository
{
    void Save(SavedPostModel savedPostModel);
    void Delete(int idSavedPost);
}