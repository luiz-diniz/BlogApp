using BlogApp.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository;

public class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;
    private readonly int _provider;
    private readonly IConfiguration _configuration;

    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = GetConnectionString();
        _provider = GetProvider();
    }

    public IDbConnection Create()
    {
        IDbConnection connection;

        switch (_provider)
        {
            case 0:
                connection = new SqlConnection(_connectionString);
                connection.Open();
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid provider.");
        }

        return connection;
    }

    private string GetConnectionString()
    {
        var connectionString = _configuration.GetSection("Database").GetSection("ConnectionStrings").GetSection("Default").Value;

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("Connection string value must be informed.");

        return connectionString;
    }

    private int GetProvider()
    {
        var provider = _configuration.GetSection("Database").GetSection("Provider").Value;

        if (string.IsNullOrWhiteSpace(provider))
            throw new Exception("Provider value must be informed.");

        if (int.TryParse(provider, out var result))
            return result;

        throw new Exception("Provider type must be integer.");
    }
}