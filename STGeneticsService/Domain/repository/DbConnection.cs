using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using STGeneticsService.Domain.Model.Entity;

public class DbConnection : ISqlConnection
{
    protected readonly IConfiguration _configuration;

    private readonly ILogger<DbConnection> _logger;
    private IDbConnection dbConnection;

    public DbConnection(IConfiguration configuration, ILogger<DbConnection> logger)
    {
        _configuration = configuration;
        _logger = logger;
        dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase"));
        if(dbConnection.State == ConnectionState.Closed)
        {
           _logger.LogInformation("Connection opened");
           dbConnection.Open();
        }
    }

     public IDbConnection OpenConnection()
    {
       dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase"));
       if(dbConnection.State == ConnectionState.Closed)
       {
           _logger.LogInformation("Connection opened");
           dbConnection.Open();
       }
       return dbConnection;
    }

    public IDbConnection CloseConnection()
    {
        dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase"));
       if(dbConnection.State == ConnectionState.Open)
       {
           dbConnection.Close();
           _logger.LogInformation("Connection closed");
       }
       return dbConnection;
    }

    public async Task<IEnumerable<T>> SelectFromTable<T>(string query)
    {
        using (dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            _logger.LogInformation("select success");
            var result = await dbConnection.QueryAsync<T>(query, commandType: CommandType.Text);
             _logger.LogInformation("result query: " + result.Count());
            if(result.Count() > 0)
            {
                _logger.LogInformation("query success");
                return result;
            }    
        }
        _logger.LogInformation("select fail");
        return Enumerable.Empty<T>();
    }

    public async Task<bool> Update(string query,  DynamicParameters param)
    {
      using (dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
       {
            if(dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            int resultUpdate = await dbConnection.ExecuteAsync(query, param, commandType: CommandType.Text);
            _logger.LogInformation("result update: " + resultUpdate);
            if(resultUpdate > 0)
            {
                _logger.LogInformation("update success");
                return true;
            }
       }
       _logger.LogInformation("update fail");
       return false; 
    }

    public async Task<bool> Delete(string nameTable, string param, int value)
    {
       using (dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
       {
          if(dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
           string sql = String.Format("DELETE FROM {0} WHERE {1} = {2}", nameTable, param, value);
           _logger.LogInformation(sql);
           int delRows = await dbConnection.ExecuteAsync(sql, commandType: CommandType.Text);
           if(delRows > 0)
           {
              _logger.LogInformation("Delete success");
              return true;
           }
       }
       _logger.LogInformation("Delete fail");
       return false; 
    }

    public async Task<bool> Insert(string query, DynamicParameters param)
    {
       using (dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
       {
              if(dbConnection.State == ConnectionState.Closed)
              {
                dbConnection.Open();
              }
              int resultInsert = await dbConnection.ExecuteAsync(query, param, commandType: CommandType.Text);
              _logger.LogInformation("result insert: " + resultInsert);
              if(resultInsert > 0)
              {
                  _logger.LogInformation("Insert success");
                  return true;
              }
       }
       _logger.LogInformation("Insert fail");
       return false; 
    }

    public async Task<int> BulkInsert<T>(string query, List<T> valueList){
        using (dbConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
        {
            if(dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            int resultBulkInsert = await dbConnection.ExecuteAsync(query, valueList, commandType: CommandType.Text);
            _logger.LogInformation("result bulk insert: " + resultBulkInsert);
            if(resultBulkInsert > 0)
            {
                _logger.LogInformation("Bulk insert success");
                return resultBulkInsert;
            }
        }
        _logger.LogInformation("Bulk insert fail");
        return 0;
    }
}