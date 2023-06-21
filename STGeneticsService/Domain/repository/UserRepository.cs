using STGeneticsService.Domain.Repository;
public class UserRepository : IUserRepository
{
    private IConfiguration _configuration;

    private ILogger _logger;

    private ISqlConnection _connection;

    public UserRepository(IConfiguration configuration, ILogger<DbConnection> logger, ISqlConnection connection)
    {
        _configuration = configuration;
        _logger = logger;
        _connection = connection;
    }

    public Users GetUser(string userName, string password)
    {
        string query = String.Format("SELECT * FROM Users WHERE username = '{0}' and password = '{1}'",userName, password);
        var taskResult =  _connection.SelectFromTable<Users>(query);
        Task.WaitAll(taskResult);
        return taskResult.Result.First();
    }
} 