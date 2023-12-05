using Shared.Classes;

namespace Server.DataAccess.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		Task<List<Order>> GetAllOrders();
		Task<List<Order>> GetOrdersForCustomer(int customerId);
		Task<bool> PlaceOrder(CustomerCart cart);
		Task<bool> CancelOrder(int id);

	}
}
