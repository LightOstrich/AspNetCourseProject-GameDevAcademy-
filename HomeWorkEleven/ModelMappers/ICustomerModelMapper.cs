using HomeWorkEleven.Models;

namespace HomeWorkEleven.ModelMappers;

public interface ICustomerModelMapper
{
    public Customer MapFromModel(CustomerModel model);
    public CustomerModel MapToModel(Customer entity);
}