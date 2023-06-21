
using STGeneticsService.Domain.Model.Entity;
using STGeneticsService.Domain.Repository;

public class OrderService : IOrderService
{
    IConfiguration _configuration;
    ILogger<AnimalService> _logger;

    IAnimalRepository _animalRepository;

    IOrderRepository _orderRepository;

    public OrderService(IConfiguration configuration, ILogger<AnimalService> logger, IAnimalRepository animalRepository, IOrderRepository orderRepository)
    {
        _configuration = configuration;
        _logger = logger;
        _animalRepository = animalRepository;
        _orderRepository = orderRepository;
    }
   
    public OrderResponseDto SaveOrder(List<OrderDto> orders)
    {
        // get the animal data with the Id 
        _logger.LogInformation("OrderService.SaveOrder");
        try{

            List<Orders> listToInsert = CreateEntityOrder(orders); 
            // save the order
            _logger.LogInformation("OrderService.SaveOrder");
            int affectedRows = _orderRepository.SaveOrder(listToInsert);
            if(affectedRows == listToInsert.Count)
            {
                return createResponseSuccess(affectedRows, listToInsert); 
            }
            else {
                return createFailResponse("There was an error saving the order");
            }
        } 
        catch (Exception ex)
        {
            _logger.LogInformation("OrderService.SaveOrder exception = " + ex.Message);
            return createFailResponse(ex.Message);
        }
    
    }

    private OrderResponseDto createResponseSuccess(int affectedRows,  List<Orders> listToInsert)
    {
        List<createdOrder> createdOrders = new List<createdOrder>();
        foreach(Orders order in listToInsert)
        {
            createdOrder createdOrder = new createdOrder();
            createdOrder.orderId = order.OrderId;
            createdOrder.totalAmount = order.TotalAmount;
            createdOrders.Add(createdOrder);
        }
        return new OrderResponseDto("success", "Order created successfully", affectedRows, createdOrders);
    }

    private OrderResponseDto createFailResponse (string message)
    {
         List<createdOrder> createdOrders = new List<createdOrder>();
        return new OrderResponseDto("fail", message, 0, createdOrders);
    }

    private List<Orders> CreateEntityOrder(List<OrderDto> orders)
    {
        Dictionary<int, Orders> animalsOrders = new Dictionary<int, Orders>();
        foreach(OrderDto order in orders)
        {
            _logger.LogInformation("OrderService.SaveOrder create order entity = " + order);
            Animal animal = _animalRepository.GetAnimalById(order.animalId);
            Orders orderEntity = new Orders();

            orderEntity.AnimalId = order.animalId;
            orderEntity.CustomerId = order.customerId;
            orderEntity.Updated = DateTime.Now;
            orderEntity.OrderDate = DateTime.Now;
            orderEntity.Quantity = order.quantity;
            orderEntity.Status = "active";
            orderEntity.OrderId = order.orderId;
            orderEntity.TotalFreight = 1000;
            // If the customer adds an animal with a quantity greater than 50 in the cart, we must apply a 5% discount on the value of this animal. 
            if(order.quantity > 50)
            {
                _logger.LogInformation("OrderService.SaveOrder order.quantity > 50");
                orderEntity.UnitPrice = (decimal)(animal.price * 0.95);
                orderEntity.TotalAmount = orderEntity.UnitPrice * order.quantity;
                orderEntity.Discount = 5;
                orderEntity.DiscountAmount = (animal.price * order.quantity) * 0.05;
            }
            // If the customer buys more than 200 animals in the order, an additional 3% discount will be added to the total purchase price.
            else if(order.quantity > 200)
            {
                _logger.LogInformation("OrderService.SaveOrder order.quantity > 200");
                orderEntity.UnitPrice = (decimal)(animal.price * 0.95);
                orderEntity.TotalAmount = (decimal)((animal.price * 0.95 * order.quantity) * 0.03);
                orderEntity.Discount = 8;
                orderEntity.DiscountAmount = (animal.price * order.quantity) * 0.08;
            }

            // If the customer buys more than 300 animals in the order, the freight value must be free, otherwise it will charge $1,000.00 for freight.
            else if(order.quantity > 300)
            {
                _logger.LogInformation("OrderService.SaveOrder order.quantity > 300");
                orderEntity.UnitPrice = (decimal)(animal.price * 0.95);
                orderEntity.TotalAmount = (decimal)((animal.price * 0.95 * order.quantity) * 0.97);
                orderEntity.Discount = 8;
                orderEntity.DiscountAmount = (animal.price * order.quantity) * 0.08;
                orderEntity.TotalFreight = 0;
            }
            else 
            {
                orderEntity.UnitPrice = (decimal)animal.price;
                orderEntity.TotalAmount = (decimal)(animal.price * order.quantity);
                orderEntity.Discount = 0;
                orderEntity.DiscountAmount = 0;
            }
            // if animalId is not in the dictionary, add it
            if(!animalsOrders.ContainsKey(order.animalId))
            {
                animalsOrders.Add(order.animalId, orderEntity);
            }
            else
            {
                // if animalId is in the dictionary, update the quantity
                string result = "AnimalId: " + order.animalId + " is already in the order";
                _logger.LogInformation(result);
                throw new Exception(result); 
            }

        }

        return animalsOrders.Values.ToList();
    }

}