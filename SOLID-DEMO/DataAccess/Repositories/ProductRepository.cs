using Server.DataAccess.Repositories.Interfaces;

namespace Server.DataAccess.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ShopContext _shopContext;

		public ProductRepository(ShopContext _shopContext)
		{
			this._shopContext = _shopContext;
		}
	}
}
