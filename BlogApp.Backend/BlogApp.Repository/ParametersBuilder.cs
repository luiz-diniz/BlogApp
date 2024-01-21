using System.Data;
using System.Data.SqlClient;

namespace BlogApp.Repository;

public static class ParametersBuilder
{
    public static void BuildSqlParameters(SqlParameterCollection sqlParameterCollection, object[] parameters)
    {
        var count = 0;

        foreach (object o in parameters)
        {            
            var sqlParameter = new SqlParameter();

            sqlParameter.ParameterName = $"P{count}";

            if (o is null)
            {
                sqlParameter.Value = DBNull.Value;
                sqlParameter.DbType = DbType.Object;
            }
            else
            {
                sqlParameter.Value = o;
                sqlParameter.DbType = GetDbType(o.GetType());
            }            

            sqlParameterCollection.Add(sqlParameter);
            count++;
        }
    }

    private static DbType GetDbType(Type type)
    {
        if (type == typeof(bool)) return DbType.Boolean;
        if (type == typeof(byte)) return DbType.Byte;
        if (type == typeof(sbyte)) return DbType.SByte;
        if (type == typeof(char)) return DbType.StringFixedLength;
        if (type == typeof(short)) return DbType.Int16;
        if (type == typeof(ushort)) return DbType.UInt16;
        if (type == typeof(int)) return DbType.Int32;
        if (type == typeof(uint)) return DbType.UInt32;
        if (type == typeof(long)) return DbType.Int64;
        if (type == typeof(ulong)) return DbType.UInt64;
        if (type == typeof(float)) return DbType.Single;
        if (type == typeof(double)) return DbType.Double;
        if (type == typeof(decimal)) return DbType.Decimal;
        if (type == typeof(DateTime)) return DbType.DateTime;
        if (type == typeof(string)) return DbType.String;

        return DbType.Object;
    }
}