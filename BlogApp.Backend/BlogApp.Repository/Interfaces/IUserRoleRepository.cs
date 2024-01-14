using BlogApp.Models;

namespace BlogApp.Repository.Interfaces;

public interface IUserRoleRepository
{
    void Add(UserRole userRole);
}