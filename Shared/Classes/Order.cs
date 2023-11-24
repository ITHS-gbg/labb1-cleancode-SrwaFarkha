namespace Shared.Classes;

public class Order
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }
    public DateTime ShippingDate { get; set; }

    public Order(int id, List<Product> products, Customer customer, DateTime shippingDate)
    {
	    Id = id;
        Products = products;
        Customer = customer;
	    ShippingDate = shippingDate;
    }

    public Order()
    {
	    
    }

}