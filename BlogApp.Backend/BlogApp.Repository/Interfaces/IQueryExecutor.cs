using System.Data;

namespace BlogApp.Repository.Interfaces;

public interface IQueryExecutor
{
    void ExecuteNonQuery(string query, object[] parameters);
    void ExecuteNonQuery(IDbConnection connection, IDbTransaction transaction, string query, object[] parameters);
    IDataReader ExecuteReader(string query);
    IDataReader ExecuteReader(string query, object[] parameters);
    object ExecuteScalar(string query, object[] parameters);
    object ExecuteScalar(IDbConnection connection, IDbTransaction transaction, string query, object[] parameters);

}