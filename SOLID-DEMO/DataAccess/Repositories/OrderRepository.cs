using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using SOLIDDEMO.Migrations;

namespace Server.DataAccess.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ShopContext _shopContext;

		public OrderRepository(ShopContext _shopContext)
		{
			this._shopContext = _shopContext;
		}

		public async Task<List<Order>> GetAllOrders()
		{
			var result = await _shopContext.Orders
				.Include(o => o.Customer)
				.Include(o => o.Products).ToListAsync();
			if (result.Count > 0)
			{
				return result;
			}

			return new List<Order>();
		}

		public async Task<List<Order>> GetOrdersForCustomer(int customerId)
		{
			var result = await _shopContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.Products)
				.Where(x => x.Customer.Id == customerId)
				.ToListAsync();
			if (result.Count > 0)
			{
				return result;
			}
			return new List<Order>();
		}

		public async Task<bool> PlaceOrder(CustomerCart cart)
		{
			var customer = await _shopContext.Customers.FirstOrDefaultAsync(c => c.Id == cart.CustomerId);

			if (customer is null)
			{
				return false;
			}

			var products = new List<Product>();

			foreach (var prodId in cart.ProductIds)
			{
				var prod = await _shopContext.Products.FirstOrDefaultAsync(p => p.Id == prodId);
				if (prod is null)
				{
					return false;
				}
				products.Add(prod);
			}

			var order = new Order() { Customer = customer, Products = products };
			var now = DateTime.Now;
			order.ShippingDate = now.AddDays(5);

			await _shopContext.Orders.AddAsync(order);
			await _shopContext.SaveChangesAsync();

			return true;
		}
	

		public async Task<bool> CancelOrder(int id)
		{
			var order = await _shopContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
			if (order == null)
			{
				return false;
			}

			_shopContext.Orders.Remove(order);
			await _shopContext.SaveChangesAsync();
			return true;
		}

	}
	}
