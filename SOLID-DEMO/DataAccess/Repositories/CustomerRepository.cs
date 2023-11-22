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

		public async Task<Customer> LoginCustomer(string email, string password)
		{
			var customer = await _shopContext.Customers
				.FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password));
			return customer; 
		}

		public bool DeleteCustomer(int id)
		{
			var customer = _shopContext.Customers.FirstOrDefault(c => c.Id == id);
			if (customer is null)
			{
				return false;
			}

			_shopContext.Customers.Remove(customer);
			_shopContext.SaveChangesAsync();

			return true;
		}
	}
}
