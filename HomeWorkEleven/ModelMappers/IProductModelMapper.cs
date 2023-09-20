using HomeWorkEleven.Data.Models;
using HomeWorkEleven.Models;

namespace HomeWorkEleven.ModelMappers;

public interface IProductModelMapper
{
    public Product MapFromModel(ProductModel model);
    public ProductModel MapToModel(Product entity);
}