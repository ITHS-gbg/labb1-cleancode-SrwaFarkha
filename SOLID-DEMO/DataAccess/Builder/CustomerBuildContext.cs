using Server.DataAccess.Builder.Interface;

namespace Server.DataAccess.Builder
{
	public class CustomerBuildContext
	{
		public void Construct(ICustomerBuilder builder)
		{
			builder.SetDiscountRate(0);
			builder.GetDiscountRate();
			builder.GetCustomer();
		}
	}
}
