using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface ISavedPostsService
{
    void Save(SavedPost savedPostModel);
    void Delete(int idSavedPost);
}