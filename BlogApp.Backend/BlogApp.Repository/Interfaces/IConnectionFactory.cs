using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IConnectionFactory
{
    IDbConnection Create();
}