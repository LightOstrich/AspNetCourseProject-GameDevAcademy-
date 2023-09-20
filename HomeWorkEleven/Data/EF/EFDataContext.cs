using HomeWorkEleven.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeWorkEleven.Data.EF;

public class EfDataContext : DbContext, IDataContext
{
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    public EfDataContext()
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    public int CreateProduct(ProductModel? productModel)
    {
        throw new NotImplementedException();
    }

    public List<ProductModel?> GetProducts()
    {
        throw new NotImplementedException();
    }

    public ProductModel GetProductById(int? id)
    {
        throw new NotImplementedException();
    }

    public int UpdateProduct(int id, ProductModel updatedProduct)
    {
        throw new NotImplementedException();
    }

    public int DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public int GetProductsCount()
    {
        throw new NotImplementedException();
    }

    public int CreateOrder(OrderModel orderModel)
    {
        throw new NotImplementedException();
    }

    public List<OrderModel?> GetOrders()
    {
        throw new NotImplementedException();
    }

    public OrderModel GetOrderById(int id)
    {
        throw new NotImplementedException();
    }

    public int UpdateOrder(int id, OrderModel updatedOrderModel)
    {
        throw new NotImplementedException();
    }

    public int DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }

    public int GetOrdersCount()
    {
        throw new NotImplementedException();
    }

    public int CreateCustomer(CustomerModel customerModel)
    {
        Customers.Add(customerModel);
        return SaveChanges();
    }

    public IList<CustomerModel?> GetCustomers()
    {
        return Customers.Cast<CustomerModel?>().ToList();
    }

    public CustomerModel GetCustomerById(int id)
    {
        throw new NotImplementedException();
    }

    public int UpdateCustomer(int id, CustomerModel customerModel)
    {
        var foundCustomer = Customers.Find(customerModel.CustomerId);

        if (foundCustomer == null)
        {
            return 0;
        }

        foundCustomer.FirstName = customerModel.FirstName;
        foundCustomer.LastName = customerModel.LastName;
        foundCustomer.Age = customerModel.Age;
        foundCustomer.Country = customerModel.Country;

        return SaveChanges();
    }

    public int DeleteCustomer(int id)
    {
        var foundCustomer = Customers.AsNoTracking().FirstOrDefault(row => row.CustomerId == id);
        if (foundCustomer == null)
        {
            return 0;
        }

        Customers.Remove(foundCustomer);
        return SaveChanges();
    }

    public int GetCustomerCount()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().Property(product => product.ProductTypeModel)
            .HasConversion(
                productType => productType.ToString(),
                productType => (ProductTypeModel)Enum.Parse(typeof(ProductTypeModel), productType)
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Datasource=localhost;Database=shop;User=root;Password=americanes_one;");
    }
}