using HomeWorkEleven.Models;

namespace HomeWorkEleven.ModelMappers;

public class CustomerModelMapper : ICustomerModelMapper
{
    public Customer MapFromModel(CustomerModel model)
    {
        var custom = new Customer()
        {
            CustomerId = model.CustomerId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Age = model.Age,
            Country = model.Country,
        };

        return custom;
    }

    public CustomerModel MapToModel(Customer entity)
    {
        var custom = new CustomerModel()
        {
            CustomerId = entity.CustomerId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Age = entity.Age,
            Country = entity.Country,
        };

        return custom;
    }
}