using BlogApp.Core.Intefaces;
using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class SavedPostsService : ISavedPostsService
{
    private readonly ILogger<SavedPostsService> _logger;
    private readonly ISavedPostsRepository _savedPostsRepository;

    public SavedPostsService(ILogger<SavedPostsService> logger, ISavedPostsRepository savedPostsRepository)
    {
        _logger = logger;
        _savedPostsRepository = savedPostsRepository;
    }

    public void Delete(int idSavedPost)
    {
        try
        {           
            _savedPostsRepository.Delete(idSavedPost);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void Save(int idPost, int idUser)
    {
        try
        {
            _savedPostsRepository.Save(idPost, idUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}