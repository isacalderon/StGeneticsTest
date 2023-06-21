using System.Data;
using Dapper;

public interface ISqlConnection
{
    IDbConnection OpenConnection();
    IDbConnection CloseConnection();

    Task<IEnumerable<T>> SelectFromTable<T>(string query); 

    Task<bool> Insert(string query, DynamicParameters param);

    Task<int> BulkInsert<T>(string query, List<T> valueList); 

    Task<bool> Update(string query, DynamicParameters param);

    Task<bool> Delete(string nameTable, string param, int value);
}