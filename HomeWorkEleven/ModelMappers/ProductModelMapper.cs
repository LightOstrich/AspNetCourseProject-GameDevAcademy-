using HomeWorkEleven.Data.Models;
using HomeWorkEleven.Models;

namespace HomeWorkEleven.ModelMappers;

public class ProductModelMapper : IProductModelMapper
{
    public Product MapFromModel(ProductModel model)
    {
        var product = new Product
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            Count = model.Count,
            ProductTypeModel = model.ProductTypeModel
        };

        return product;
    }

    public ProductModel MapToModel(Product entity)
    {
        var product = new ProductModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            Count = entity.Count,
            ProductTypeModel = entity.ProductTypeModel
        };
        return product;
    }
}