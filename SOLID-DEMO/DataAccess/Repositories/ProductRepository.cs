using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;

namespace Server.DataAccess.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ShopContext _shopContext;

		public ProductRepository(ShopContext _shopContext)
		{
			this._shopContext = _shopContext;
		}


		public async Task<List<Product>> GetProducts()
		{
			var products = await _shopContext.Products.ToListAsync();
			if (products.Count > 0)
			{
				return products;

			}

			return new List<Product>();
		}

		public async Task<Product> GetProduct(int id)
		{
			var product = await _shopContext.Products
				.FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				return null;
			}
			return product;
		}


		public async Task<Product> AddProduct(Product product)
		{
			await _shopContext.Products.AddAsync(product);
			await _shopContext.SaveChangesAsync();
			return product;
		}
	}
}
