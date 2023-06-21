using Dapper;
using STGeneticsService.Domain.Model.Entity;
using STGeneticsService.Domain.Repository;

public class AnimalRepository : IAnimalRepository
{
     protected readonly IConfiguration _configuration;

    private readonly ILogger<DbConnection> _logger;

    private ISqlConnection _connection;

    public AnimalRepository(IConfiguration configuration, ILogger<DbConnection> logger, ISqlConnection connection)
    {
        _configuration = configuration;
        _logger = logger;
        _connection = connection;
    }
    public async Task<bool> DeleteAnimal(int id)
    {
       return await _connection.Delete("animal", "animal_id", id);
    }

    public Animal GetAnimalById(int id)
    {
       string query = "SELECT * FROM animal Where animal_id = " + id.ToString();
         var taskResult = _connection.SelectFromTable<Animal>(query);
         Task.WaitAll(taskResult);
         return taskResult.Result.First();
    }
    public IEnumerable<Animal> GetAnimalsbySex(int sex)
    {
        string query = "SELECT * FROM animal Where sex = " + sex.ToString();
        var taskResult = _connection.SelectFromTable<Animal>(query); 
        Task.WaitAll(taskResult);
        return taskResult.Result;
    }

    public IEnumerable<Animal> GetAnimalsbyStatus(int status)
    {
        string query = "SELECT * FROM animal Where status = " + status;
        var taskResult = _connection.SelectFromTable<Animal>(query); 
        Task.WaitAll(taskResult);
        return taskResult.Result; 
    }

    public IEnumerable<Animal> GetAnimalsbyName(string name)
    {
        string query = "SELECT * FROM animal Where name = '" + name + "'";
        var taskResult = _connection.SelectFromTable<Animal>(query); 
        Task.WaitAll(taskResult);
        return taskResult.Result;
    }

    public Task<bool> InsertAnimal(Animal animal)
    {
        string query = @"INSERT INTO dbo.animal(name, breed, birth_date, sex, price, status) VALUES(@name, @breed, @birthdate, @sex, @price, @status)";
        _logger.LogInformation("AnimalRepository.InsertAnimal: " + query); 
        var param = new DynamicParameters();
            param.Add("@name", animal.name); 
            param.Add("@breed", animal.breed);
            param.Add("@birthdate", animal.birthDate);
            param.Add("@sex", animal.sex); 
            param.Add("@price", animal.price);
            param.Add("@status", animal.status);
          
         var taskResult= _connection.Insert(query, param);
        _logger.LogInformation("result InsertAnimal ");
        return taskResult;
    }

    public Task<bool> UpdateAnimal(Animal animal)
    {
        // get the animal by id
        string query = @"UPDATE animal SET name = @name, breed = @breed, birth_date = @birthdate, sex = @sex,price = @price, status = @status WHERE animal_id = @id";
        _logger.LogInformation("AnimalRepository.UpdateAnimal: " + query); 
        var param = new DynamicParameters();
            param.Add("@name", animal.name); 
            param.Add("@breed", animal.breed);
            param.Add("@birthdate", animal.birthDate);
            param.Add("@sex", animal.sex); 
            param.Add("@price", animal.price);
            param.Add("@status", animal.status);
            param.Add("@id", animal.animalId);
          
         var taskResult= _connection.Update(query, param);
        // update the animal
        return taskResult;
    }
}