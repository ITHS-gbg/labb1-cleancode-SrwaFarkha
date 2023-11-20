using Server.DataAccess.Repositories.Interfaces;

namespace Server.DataAccess.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ShopContext _shopContext;

		public OrderRepository(ShopContext _shopContext)
		{
			this._shopContext = _shopContext;
		}
	}
}
