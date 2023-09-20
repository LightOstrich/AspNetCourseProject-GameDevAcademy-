namespace HomeWorkEleven.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public DateTime DateTime { get; set; }
}