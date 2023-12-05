using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;

namespace Server.DataAccess.Repositories
{
	public class ShoppingCartRepository : IShoppingCartRepository
	{
		private readonly ShopContext _shopContext;

		public ShoppingCartRepository(ShopContext _shopContext)
		{
			this._shopContext = _shopContext;
		}
		public async Task<bool> AddProductToShoppingCart(CustomerCart itemsToAdd)
		{
			var customerCart = await _shopContext.ShoppingCarts
				.FirstOrDefaultAsync(c => c.CustomerId.Equals(itemsToAdd.CustomerId));
			if (customerCart is null)
			{
				return false;
			}

			foreach (var prodId in itemsToAdd.ProductIds)
			{
				var prod = await _shopContext.Products.FirstOrDefaultAsync(p => p.Id == prodId);
				if (prod is null)
				{
					return false;
				}
				customerCart.ProductIds.Add(prodId);
			}
			await _shopContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteProductFromShoppingCart(CustomerCart itemsToRemove)
		{
			var customerCart = await _shopContext.ShoppingCarts
				.FirstOrDefaultAsync(c => c.CustomerId.Equals(itemsToRemove.CustomerId));
			if (customerCart is null)
			{
				return false;
			}

			foreach (var prodId in itemsToRemove.ProductIds)
			{
				customerCart.ProductIds.Remove(prodId);
			}

			await _shopContext.SaveChangesAsync();
			return true;
		}
	}
}
