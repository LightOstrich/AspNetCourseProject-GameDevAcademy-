using HomeWorkEleven.Models;

namespace HomeWorkEleven.ModelMappers;

public interface IOrderModelMapper
{
    public Order MapFromModel(OrderModel model);
    public OrderModel MapToModel(Order entity);
}