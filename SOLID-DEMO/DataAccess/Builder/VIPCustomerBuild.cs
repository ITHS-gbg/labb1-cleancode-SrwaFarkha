using Server.DataAccess.Builder.Interface;
using Shared.Classes;
using Shared.Enums;


namespace Server.DataAccess.Builder
{
	public class VIPCustomerBuild : ICustomerBuilder
	{
		private Customer customer;

		public VIPCustomerBuild( string name, string password, string email)
		{
			customer = new Customer( name, password, email, Enums.CustomerLevel.VIP);
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
