using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using System.ComponentModel.DataAnnotations;

namespace Server.DataAccess.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly ShopContext _shopContext;

		public CustomerRepository(ShopContext _shopContext)
		{
			this._shopContext = _shopContext;
		}

		public async Task<List<Customer>> GetAll()
		{
			var customers = await _shopContext.Customers.ToListAsync();
			return customers;
		}

		public async Task<Customer> GetByEmailAddress(string email)
		{
			var account = await _shopContext.Customers
				.FirstOrDefaultAsync(x => x.Email == email);

			return account;
		}

		public async Task<Customer> RegisterCustomer(Customer customer)
		{
			await _shopContext.Customers.AddAsync(customer);
			await _shopContext.SaveChangesAsync();
			return customer;
		}
	}
}
