using Server.DataAccess.Repositories.Interfaces;

namespace Server.DataAccess.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ShopContext _shopContext;

		public UnitOfWork(ShopContext shopContext)
		{
			_shopContext = shopContext;
		}

		public ICustomerRepository CustomerRepository =>
			new CustomerRepository(_shopContext);

		public IOrderRepository OrderRepository =>
			new OrderRepository(_shopContext);

		public IProductRepository ProductRepository =>
			new ProductRepository(_shopContext);
	}
}