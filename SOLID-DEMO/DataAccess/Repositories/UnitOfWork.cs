using Server.DataAccess.Builder;
using Server.DataAccess.Repositories.Interfaces;

namespace Server.DataAccess.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ShopContext _shopContext;
		private readonly CustomerBuildContext _customerBuildContext;

		public UnitOfWork(ShopContext shopContext, CustomerBuildContext customerBuildContext)
		{
			_shopContext = shopContext;
			_customerBuildContext = customerBuildContext;
		}

		public ICustomerRepository CustomerRepository =>
			new CustomerRepository(_shopContext, _customerBuildContext);

		public IOrderRepository OrderRepository =>
			new OrderRepository(_shopContext);

		public IProductRepository ProductRepository =>
			new ProductRepository(_shopContext);

		public IShoppingCartRepository ShoppingCartRepository =>
			new ShoppingCartRepository(_shopContext);
	}
}