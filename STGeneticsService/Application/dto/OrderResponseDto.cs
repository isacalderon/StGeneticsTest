public class OrderResponseDto{

   public string status { get; set; }
   public string? message { get; set; }

   public int totalOrders { get; set; }

   public List<createdOrder>? createdOrders { get; set; }

    public OrderResponseDto(string status, string message, int totalOrders, List<createdOrder> createdOrders)
    {
        this.status = status;
        this.message = message;
        this.totalOrders = totalOrders;
        this.createdOrders = createdOrders;
    }

    
}

public class createdOrder {
    public int orderId { get; set; }
    public decimal totalAmount { get; set; }
}