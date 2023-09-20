using HomeWorkEleven.Models;

namespace HomeWorkEleven.Data;

public interface IDataContext
{
    //CRUD PRODUCT OPERATION
    public int CreateProduct(ProductModel? productModel);
    public List<ProductModel?> GetProducts();
    public ProductModel GetProductById(int? id);
    public int UpdateProduct(int id, ProductModel updatedProduct);
    public int DeleteProduct(int id);

    public int GetProductsCount();

    //CRUD ORDERS OPERATION
    public int CreateOrder(OrderModel orderModel);
    public List<OrderModel?> GetOrders();
    public OrderModel GetOrderById(int id);
    public int UpdateOrder(int id, OrderModel updatedOrderModel);
    public int DeleteOrder(int id);

    public int GetOrdersCount();

    //CRUD CUSTOMER OPERATION
    public int CreateCustomer(CustomerModel customerModel);
    public IList<CustomerModel?> GetCustomers();
    public CustomerModel GetCustomerById(int id);
    public int UpdateCustomer(int id, CustomerModel customerModel);
    public int DeleteCustomer(int id);

    public int GetCustomerCount();
}