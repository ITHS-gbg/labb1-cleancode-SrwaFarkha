using Server.DataAccess.Builder.Interface;
using Shared.Classes;

namespace Server.DataAccess.Builder.Interface

{
    public interface ICustomerBuilder
    {
		void SetDiscountRate(double discountRate);

		double GetDiscountRate();

		Customer GetCustomer();
		Customer Build();
	}
}




