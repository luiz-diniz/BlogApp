using BlogApp.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository.SqlRepository;

public class SqlQueryExecutor : IQueryExecutor
{
    private readonly IConnectionFactory _connectionFactory;

    public SqlQueryExecutor(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void ExecuteNonQuery(string query, object[] parameters)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public void ExecuteNonQuery(IDbConnection connection, IDbTransaction transaction, string query, object[] parameters)
    {
        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
    }

    public IDataReader ExecuteReader(string query)
    {
        var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        cmd.CommandType = CommandType.Text;

        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public IDataReader ExecuteReader(string query, object[] parameters)
    {
        var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public object ExecuteScalar(string query, object[] parameters)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var cmd = new SqlCommand(query, connection as SqlConnection);

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        return cmd.ExecuteScalar();
    }

    public object ExecuteScalar(IDbConnection connection, IDbTransaction transaction, string query, object[] parameters)
    {
        using var cmd = new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);

        ParametersBuilder.BuildSqlParameters(cmd.Parameters, parameters);

        cmd.CommandType = CommandType.Text;

        return cmd.ExecuteScalar();
    }
}