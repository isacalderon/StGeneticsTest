using Dapper;
using STGeneticsService.Domain.Model.Entity;
using STGeneticsService.Domain.Repository;
public class OrderRepository : IOrderRepository
{
    private IConfiguration _configuration;

    private ILogger _logger;

    private ISqlConnection _connection;

    public OrderRepository(IConfiguration configuration, ILogger<DbConnection> logger, ISqlConnection connection)
    {
        _configuration = configuration;
        _logger = logger;
        _connection = connection;
    }
    public IEnumerable<Orders> GetOrdersByFilter(string filter, string value)
    {
        throw new NotImplementedException();
    }

    public int SaveOrder(List<Orders> order)
    {
        _logger.LogInformation("OrderRepository.SaveOrder");
        string query = "INSERT INTO Orders (order_id, customer_id, animal_id, order_date, updated, quantity, discount, discount_amount, total_amount, unit_price, total_freight, status)" +
                       " VALUES (@OrderId, @CustomerId, @AnimalId, @OrderDate, @Updated, @Quantity, @Discount, @DiscountAmount, @TotalAmount, @UnitPrice, @TotalFreight, @Status)"; 
        var taskResult =  _connection.BulkInsert<Orders>(query, order);
        Task.WaitAll(taskResult);
        return taskResult.Result;
    }

 
}