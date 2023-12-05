using Server.DataAccess.Builder.Interface;
using Shared.Classes;
using Shared.Enums;


namespace Server.DataAccess.Builder
{
    public class StandardCustomerBuild : ICustomerBuilder
    {
	    private Customer customer;

	    public StandardCustomerBuild(string name, string password, string email)
	    {
		    customer = new Customer(name, password, email, Enums.CustomerLevel.Standard);
	    }

		public void SetDiscountRate(double discountRate)
	    {
		    customer.DiscountRate = discountRate;
	    }

	    public double GetDiscountRate()
	    {
		    return customer.DiscountRate;
	    }

	    public Customer GetCustomer()
	    {
		    return customer;
	    }

	    public Customer Build()
	    {
		    return customer;
	    }
	}
}
