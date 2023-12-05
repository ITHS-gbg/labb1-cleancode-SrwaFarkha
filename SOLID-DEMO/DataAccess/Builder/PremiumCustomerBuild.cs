using Server.DataAccess.Builder.Interface;
using Shared.Classes;
using Shared.Enums;

namespace Server.DataAccess.Builder
{
    public class PremiumCustomerBuild : ICustomerBuilder
    {
	    private Customer customer;

	    public PremiumCustomerBuild(string name, string password, string email)
	    {
		    customer = new Customer(name, password, email, Enums.CustomerLevel.Premium);
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
