using Shared.Classes;

namespace Server.DataAccess.Repositories.Interfaces
{
	public interface IShoppingCartRepository
	{
		Task<bool> AddProductToShoppingCart(CustomerCart itemsToAdd);
		Task<bool> DeleteProductFromShoppingCart(CustomerCart itemsToRemove);
	}
}
