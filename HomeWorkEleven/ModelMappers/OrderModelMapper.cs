using HomeWorkEleven.Models;

namespace HomeWorkEleven.ModelMappers;

public class OrderModelMapper : IOrderModelMapper
{
    public Order MapFromModel(OrderModel model)
    {
        var order = new Order()
        {
            OrderId = model.OrderId,
            CustomerId = model.CustomerId,
            ProductId = model.ProductId,
            Amount = model.Amount,
            DateTime = model.DateTime,
        };

        return order;
    }

    public OrderModel MapToModel(Order entity)
    {
        var order = new OrderModel()
        {
            OrderId = entity.OrderId,
            CustomerId = entity.CustomerId,
            ProductId = entity.ProductId,
            Amount = entity.Amount,
            DateTime = entity.DateTime,
        };

        return order;
    }
}