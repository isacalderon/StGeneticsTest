public class Orders
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int AnimalId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime Updated { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }

    public decimal TotalFreight { get; set; }
    public string? Status { get; set; }
    public double DiscountAmount { get; internal set; }
}
