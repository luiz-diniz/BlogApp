using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface ISavedPostsService
{
    void Save(SavedPostModel savedPostModel);
    void Delete(int idSavedPost);
}