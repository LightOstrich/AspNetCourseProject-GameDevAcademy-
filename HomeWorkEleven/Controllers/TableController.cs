using HomeWorkEleven.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWorkEleven.Controllers;

[Route("[controller]")]
public class TableController : Controller
{
    private static List<ProductModel?> ProductModels { get; set; } = new();
    private readonly Random _rnd = new();
    private static string[]? Name { get; set; }
    private static string[]? Title { get; set; }
    private static int Id { get; set; }

    public TableController()
    {
        Name = new[]
        {
            "автостопом по галактике", "Властелин Колец", "Велосипед - AKa2000", "Puma", "Война и Мир",
            "средство для мытья волос", "Lenovo Legion", "Xiaomi Mi 8", "Джинсы Louis Vuitton",
            "Язык программирования C++",
            "Искусство программирования", "Конкретная математика", "Крестный Отец", "Форсаж", "Божественная комедия",
            "HHH1242", "Монитор Асус ", "кроссовки Nike", "Фен Дайсон", "barbie",
            "rolex", "Ведьмак", "New York Times", "red dead redemption 2", "Сборник песен - Классика",
        };
        Title = new[]
        {
            "Книга", "Фильм", "Сериал", "Музыка", "Игра",
            "Спортинвентарь", "Журнал", "Радио", "Велосипед", "Сумка",
            "Компьютер", "Часы", "Телефон", "Техника", "Косметика",
            "Посуда", "Инструменты", "Детские игрушки,", "Обувь", "Аксессуары",
        };
    }

    private string? RndUniqueNames(string[]? strings, List<int> indexes)
    {
        var index = _rnd.Next(indexes.Count);
        var value = strings?[indexes[index]];
        indexes.RemoveAt(index);
        return value;
    }

    private ProductTypeModel GetRandomProductType()
    {
        var values = Enum.GetValues(typeof(ProductTypeModel));
        var rndTypeOfProduct = (ProductTypeModel)values.GetValue(_rnd.Next(values.Length))!;
        return rndTypeOfProduct;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        if (ProductModels.Count <= 0)
        {
            var products = GenerateProducts();
            return View("~/Views/Table/TableProducts.cshtml", products);
        }

        return View("~/Views/Table/TableProducts.cshtml", ProductModels);
    }

    [HttpGet("GenerateProducts")]
    public List<ProductModel?> GenerateProducts()
    {
        var namesIndexes = Enumerable.Range(0, Name!.Length).ToList();
        for (var i = 0; i <= 20; i++)
        {
            var id = Id++;
            var title = Title?[_rnd.Next(Title.Length)];
            var description = RndUniqueNames(Name, namesIndexes);
            var price = /*Math.Round(_rnd.NextDouble() * (10000 - 1) + 1, 2)*/ double.NaN;
            var count = _rnd.Next(0, 1000);
            var typeofProduct = GetRandomProductType();
            var product = new ProductModel(id, title, description, price, count, typeofProduct);
            ProductModels.Add(product);
        }

        return ProductModels;
    }

    [HttpPost("CreateProduct")]
    public IActionResult CreateProduct([FromBody] ProductModel? product)
    {
        if (product != null)
        {
            GenerateId(product);
            ProductModels.Add(product);
            return Ok(product);
        }

        return BadRequest();
    }

    [HttpPost("UpdateProduct/{id:int}")]
    public IActionResult UpdateProduct([FromBody] ProductModel updatedProduct, [FromRoute] int id)
    {
        var product = ProductModels.FirstOrDefault(p => p!.Id == id);
        if (product != null)
        {
            product.Title = updatedProduct.Title;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Count = updatedProduct.Count;

            return Ok(product);
        }

        return NotFound();
    }

    [HttpDelete("DeleteProduct/{id:int}")]
    public IActionResult DeleteProduct([FromRoute] int id)
    {
        var product = ProductModels.FirstOrDefault(p => p!.Id == id);
        if (product != null)
        {
            ProductModels.Remove(product);
            return Ok();
        }

        return NotFound();
    }

    [HttpPost("GenerateID")]
    public void GenerateId(ProductModel? productModel)
    {
        productModel!.Id = Id++;
    }

    [HttpGet("GetProductById")]
    public ProductModel? GetProduct(int id)
    {
        return ProductModels.FirstOrDefault(i => i!.Id == id);
    }
}