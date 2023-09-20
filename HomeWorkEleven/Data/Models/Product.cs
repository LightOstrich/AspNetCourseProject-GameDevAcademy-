using System.Text.Json.Serialization;
using HomeWorkEleven.Models;

namespace HomeWorkEleven.Data.Models;

public class Product
{
    public Product(int id, string? title, string? description, double price, int count, ProductTypeModel productTypeModel)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        Count = count;
        ProductTypeModel = productTypeModel;
    }

    public Product()
    {
    }

    [JsonInclude] public int Id { get; set; }

    [JsonInclude] public string? Title { get; set; }

    [JsonInclude] public string? Description { get; set; }

    [JsonInclude] public double Price { get; set; }

    [JsonInclude] public int Count { get; set; }
    [JsonInclude] public ProductTypeModel ProductTypeModel { get; set; }
}