using HomeWorkEleven.Data;
using HomeWorkEleven.Data.Ado;
using HomeWorkEleven.Data.Models;
using HomeWorkEleven.ModelMappers;
using HomeWorkEleven.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace HomeWorkEleven.Controllers;

public class TestController : ControllerBase
{
    private readonly IDataContext _dataContext;
    private IProductModelMapper _productModelMapper = new ProductModelMapper();

    public TestController()
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "shop",
            UserID = "root",
            Password = "americanes_one"
        };
        _dataContext = new AdoConnectedDataContext(connectionStringBuilder.ConnectionString);
    }

    

    //Customers
    [HttpGet("customers")]
    public IList<CustomerModel?> GetCustomers()
    {
        return _dataContext.GetCustomers();
    }

    [HttpGet("GetCustomerCount")]
    public int GetCustomerCount()
    {
        return _dataContext.GetCustomerCount();
    }

    [HttpDelete("Delete customer")]
    public int DeleteCustomer(int id)
    {
        return _dataContext.DeleteCustomer(id);
    }

    [HttpPost("Create customer")]
    public int CreateCustomer([FromForm] CustomerModel customerModel)
    {
        return _dataContext.CreateCustomer(customerModel);
    }

    [HttpGet("GetCustomerById")]
    public CustomerModel GetCustomerById(int id)
    {
        return _dataContext.GetCustomerById(id);
    }

    [HttpPut("UpdateCustomer")]
    public int UpdateCustomer(int id, [FromBody] CustomerModel customerModel)
    {
        return _dataContext.UpdateCustomer(id, customerModel);
    }

    //Orders
    [HttpGet("orders")]
    public IList<OrderModel?> GetOrders()
    {
        return _dataContext.GetOrders();
    }

    [HttpGet("GetOrdersCount")]
    public int GetOrdersCount()
    {
        return _dataContext.GetOrdersCount();
    }

    [HttpPost("CreateOrder")]
    public int CreateOrder([FromBody] OrderModel orderModel)
    {
        return _dataContext.CreateOrder(orderModel);
    }

    [HttpGet("GetOrderById")]
    public OrderModel GetOrderById(int id)
    {
        return _dataContext.GetOrderById(id);
    }

    [HttpGet("DeleteOrder")]
    public int DeleteOrder(int id)
    {
        return _dataContext.DeleteOrder(id);
    }

    [HttpDelete("UpdateOrder")]
    public int UpdateOrder(int id, OrderModel orderModel)
    {
        return _dataContext.UpdateOrder(id, orderModel);
    }


    //Products
    [HttpGet("Get products")]
    public IList<ProductModel?> GetProducts()
    {
        return _dataContext.GetProducts();
    }

    [HttpGet("Get productsById")]
    public ProductModel GetProducts(int id)
    {
        return _dataContext.GetProductById(id);
    }

    [HttpPost("CreateProduct")]
    public int CreateProduct([FromBody] Product productModel)
    {
        //return _dataContext.CreateProduct(productModel);
        var dbProduct = _productModelMapper.MapToModel(productModel);
        return _dataContext.CreateProduct(dbProduct);
    }

    [HttpPost("Update")]
    public int UpdateProduct(int id, [FromBody] ProductModel productModel)
    {
        return _dataContext.UpdateProduct(id, productModel);
    }

    [HttpDelete("DeleteProduct")]
    public int DeleteProduct(int id)
    {
        return _dataContext.DeleteProduct(id);
    }

    [HttpGet("GetProductsCount")]
    public int GetProductsCount()
    {
        return _dataContext.GetProductsCount();
    }
}