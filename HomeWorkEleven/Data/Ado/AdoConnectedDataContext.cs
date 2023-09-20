using System.Data;
using HomeWorkEleven.Models;
using MySqlConnector;

namespace HomeWorkEleven.Data.Ado;

public class AdoConnectedDataContext : IDataContext
{
    private readonly DataSet _dataSet = new();
    private readonly string _connectionString;

    private DataTable? Customers => _dataSet.Tables["customer"];
    private DataTable? Product => _dataSet.Tables["product"];
    private DataTable? Orders => _dataSet.Tables["orders"];

    public AdoConnectedDataContext(string connectionString)
    {
        _connectionString = connectionString;
        //TODO:* В конструкторе AdoConnectedDataContext проверить существование таблиц и в случае их отсутствия считать из файлов и выполнить скрипты
        CheckAndCreateTables();
        Init();
    }

    private void CheckAndCreateTables()
    {
        if (!(TableExists("customer")) || !(TableExists("customer")) || !(TableExists("customer")))
        {
            var createScript =
                File.ReadAllText(@"C:\Users\Lenovo-PC\RiderProjects\HomeWorkEleven\HomeWorkEleven\SQLSCRIPT\SHOP.sql");
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = createScript;
            command.ExecuteNonQuery();
        }
    }

    private bool TableExists(string tableName)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = \"@tableName\"";
        command.Parameters.AddWithValue("@tableName", tableName);
        var count = (int)command.ExecuteScalar()!;
        return count > 0;
    }

    private void Init()
    {
        _dataSet.Clear();
        using var connection = new MySqlConnection(_connectionString);
        var customerAdapter = new MySqlDataAdapter("SELECT * FROM customer", connection);
        var productsAdapter = new MySqlDataAdapter("SELECT * FROM product", connection);
        var ordersAdapter = new MySqlDataAdapter("SELECT * FROM orders", connection);

        customerAdapter.Fill(_dataSet, "customer");
        productsAdapter.Fill(_dataSet, "product");
        ordersAdapter.Fill(_dataSet, "orders");
    }

    public int CreateProduct(ProductModel? productModel)
    {
        if (productModel == null)
        {
            return 0;
        }

        var row = Product?.NewRow();
        if (row != null)
        {
            row["product_id"] = productModel.Id;
            row["Title"] = productModel.Title;
            row["Description"] = productModel.Description;
            row["Price"] = productModel.Price;
            row["Count"] = productModel.Count;
            row["product_type"] = productModel.ProductTypeModel.ToString();
            Product?.Rows.Add(row);
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            var productsAdapter = new MySqlDataAdapter("SELECT * FROM product", connection);
            var productBuilder = new MySqlCommandBuilder(productsAdapter);
            productsAdapter.Update(Product!);
        }

        Init();
        return 1;
    }

    public List<ProductModel?> GetProducts()
    {
        var result = new List<ProductModel?>();
        if (Product == null)
        {
            return result;
        }

        var rows = Product.Select();
        result.AddRange(rows.Select(row => new ProductModel
        {
            Id = (int)row[0],
            Title = (string)row[1],
            Description = (string)row[2],
            Price = (double)row[3],
            Count = (int)row[4],
            ProductTypeModel = GetProductType(row[5])
        }));

        return result;
    }

    private ProductTypeModel GetProductType(object obj)
    {
        var enums = Enum.GetValues<ProductTypeModel>();
        foreach (var type in enums)
        {
            if (type.ToString() == obj.ToString())
            {
                return type;
            }
        }

        return default;
    }

    public ProductModel GetProductById(int? id)
    {
        if (id.HasValue == false)
        {
            return null!;
        }

        var row = Product?.Select($"product_id ={id}").FirstOrDefault();
        var product = new ProductModel
        {
            Id = (int)row![0],
            Title = (string)row[1],
            Description = (string)row[2],
            Price = (double)row[3],
            Count = (int)row[4],
            ProductTypeModel = GetProductType(row[5])
        };
        return product;
    }

    public int UpdateProduct(int id, ProductModel updatedProduct)
    {
        /*const string query =
            "UPDATE product SET TITLE = @Title,Description = @Description,Price = @Price,Count = @Count,product_type = @ProductType WHERE product_id = @id";
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Title", updatedProduct.Title);
        command.Parameters.AddWithValue("@Description", updatedProduct.Description);
        command.Parameters.AddWithValue("@Price", updatedProduct.Price);
        command.Parameters.AddWithValue("@Count", updatedProduct.Count);
        command.Parameters.AddWithValue("@product_type", updatedProduct.ProductType);
        command.Parameters.AddWithValue("@product_id", id);
        Init();
        command.ExecuteNonQuery();*/

        var row = Product?.Select($"product_id ={id}").FirstOrDefault();
        if (row != null)
        {
            row["Title"] = updatedProduct.Title;
            row["Description"] = updatedProduct.Description;
            row["Price"] = updatedProduct.Price;
            row["Count"] = updatedProduct.Count;
            row["product_type"] = updatedProduct.ProductTypeModel.ToString();
        }

        using var connection = new MySqlConnection(_connectionString);
        var productsAdapter = new MySqlDataAdapter("SELECT * FROM product", connection);
        var productBuilder = new MySqlCommandBuilder(productsAdapter);
        productsAdapter.Update(Product!);

        return 1;
    }

    public int DeleteProduct(int id)
    {
        var row = Product?.Select($"product_id ={id}").FirstOrDefault();
        if (row == null)
        {
            return 0;
        }

        row.Delete();
        using var connection = new MySqlConnection(_connectionString);
        var productsAdapter = new MySqlDataAdapter("SELECT * FROM product", connection);
        productsAdapter.Update(Product!);
        Init();
        return 1;
    }

    public int GetProductsCount()
    {
        return Product != null ? Product.Columns.Count : 0;
    }

    public int CreateOrder(OrderModel orderModel)
    {
        var newRow = Orders?.NewRow();
        if (newRow != null)
        {
            newRow["order_id"] = orderModel.OrderId;
            newRow["customer_id"] = orderModel.CustomerId;
            newRow["product_id"] = orderModel.ProductId;
            newRow["amount"] = orderModel.Amount;
            newRow["created_at"] = orderModel.DateTime;
            Orders?.Rows.Add(newRow);
            using (var connection = new MySqlConnection(_connectionString))
            {
                var adapter = new MySqlDataAdapter("SELECT * FROM product", connection);
                var productBuilder = new MySqlCommandBuilder(adapter);
                adapter.Update(Orders!);
            }

            Init();
            return 1;
        }

        return 0;
    }

    public List<OrderModel?> GetOrders()
    {
        var result = new List<OrderModel?>();
        if (Product == null)
        {
            return result;
        }

        var rows = Product.Select();
        result.AddRange(rows.Select(row => new OrderModel
        {
            OrderId = (int)row[0],
            CustomerId = (int)row[1],
            ProductId = (int)row[2],
            Amount = (int)row[3],
            DateTime = (DateTime)row[4]
        }));

        return result;
    }

    public OrderModel GetOrderById(int id)
    {
        var row = Orders?.Select($"order_id ={id}").FirstOrDefault();
        if (row == null)
        {
            return null!;
        }

        var order = new OrderModel
        {
            OrderId = (int)row[0],
            CustomerId = (int)row[1],
            ProductId = (int)row[2],
            Amount = (int)row[3],
            DateTime = (DateTime)row[4]
        };
        return order;
    }

    public int UpdateOrder(int id, OrderModel updatedOrderModel)
    {
        var row = Product?.Select($"product_id ={id}").FirstOrDefault();
        if (row != null)
        {
            row["order_id"] = updatedOrderModel.OrderId;
            row["customer_id"] = updatedOrderModel.CustomerId;
            row["product_id"] = updatedOrderModel.ProductId;
            row["amount"] = updatedOrderModel.Amount;
            row["created_at"] = updatedOrderModel.DateTime;
        }

        using var connection = new MySqlConnection(_connectionString);
        var productsAdapter = new MySqlDataAdapter("SELECT * FROM product", connection);
        productsAdapter.Update(Product!);

        return 1;
    }

    public int DeleteOrder(int id)
    {
        var row = Orders?.Select($"order_id ={id}").FirstOrDefault();
        if (row == null)
        {
            return 0;
        }

        row.Delete();
        using (var connection = new MySqlConnection(_connectionString))
        {
            var adapter = new MySqlDataAdapter("SELECT * FROM product", connection);
            var productBuilder = new MySqlCommandBuilder(adapter);
            adapter.Update(Orders!);
        }

        Init();
        return 1;
    }

    public int GetOrdersCount()
    {
        return Orders != null ? Orders.Rows.Count : 0;
    }

    public int CreateCustomer(CustomerModel customerModel)
    {
        if (customerModel == null)
        {
            return 0;
        }

        var row = Customers?.NewRow();
        if (row != null)
        {
            row["customer_id"] = customerModel.CustomerId;
            row["first_name"] = customerModel.FirstName;
            row["last_name"] = customerModel.LastName;
            row["age"] = customerModel.Age;
            row["country"] = customerModel.Country;
            Customers?.Rows.Add(row);
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            var customerAdapter = new MySqlDataAdapter("SELECT * FROM customer", connection);
            customerAdapter.Update(Customers!);
        }

        Init();
        return 1;
    }

    public IList<CustomerModel?> GetCustomers()
    {
        var result = new List<CustomerModel>();
        if (Customers == null)
        {
            return null!;
        }

        var rows = Customers.Select();
        foreach (DataRow row in rows)
        {
            var customer = new CustomerModel
            {
                CustomerId = (int)row[0],
                FirstName = (string)row[1],
                LastName = (string)row[2],
                Age = (int)row[3],
                Country = (string)row[4]
            };
            result.Add(customer);
        }

        return result;
    }

    public CustomerModel GetCustomerById(int id)
    {
        var row = Product?.Select($"customer_id ={id}").FirstOrDefault();
        if (row == null)
        {
            return null;
        }

        var customer = new CustomerModel()
        {
            CustomerId = (int)row[1],
            FirstName = (string)row[2],
            LastName = (string)row[3],
            Age = (int)row[4],
            Country = (string)row[5]
        };
        return customer;
    }

    public int UpdateCustomer(int id, CustomerModel customerModel)
    {
        var query =
            $"UPDATE customer SET first_name = @first_name,last_name = @last_name,age = @age,country = @country WHERE customer_id = {id}";
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@first_name", customerModel.FirstName);
        command.Parameters.AddWithValue("@last_name", customerModel.LastName);
        command.Parameters.AddWithValue("@age", customerModel.Age);
        command.Parameters.AddWithValue("@country", customerModel.Country);
        /*command.Parameters.AddWithValue("@customer_id", id);*/
        Init();
        command.ExecuteNonQuery();
        return 1;
    }

    public int DeleteCustomer(int id)
    {
        var row = Customers?.Select($"customer_id ={id}").FirstOrDefault();
        if (row == null)
        {
            return 0;
        }

        row.Delete();
        using (var connection = new MySqlConnection(_connectionString))
        {
            var customersAdapter = new MySqlDataAdapter("SELECT * FROM customer", connection);
            customersAdapter.Update(Customers!);
        }

        Init();
        return 1;
    }

    public int GetCustomerCount()
    {
        return Customers != null ? Customers.Columns.Count : 0;
    }
}