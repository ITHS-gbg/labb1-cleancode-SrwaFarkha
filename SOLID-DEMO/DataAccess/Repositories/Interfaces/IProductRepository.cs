using Shared.Classes;

namespace Server.DataAccess.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetProducts();
		Task<Product> GetProduct(int id);
		Task<Product> AddProduct(Product product);
	}
}
