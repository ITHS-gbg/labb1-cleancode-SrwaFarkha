using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using System.ComponentModel.DataAnnotations;
using Server.DataAccess.Builder;

namespace Server.DataAccess.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly ShopContext _shopContext;
		private readonly CustomerBuildContext _buildContext;

		public CustomerRepository(ShopContext _shopContext, CustomerBuildContext _buildContext)
		{
			this._shopContext = _shopContext;
			this._buildContext = _buildContext;
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

		public async Task<Customer> RegisterPremiumCustomer(Customer customer)
		{
			var premiumCustomerBuild = new PremiumCustomerBuild(customer.Name, customer.Password, customer.Email);
			
			_buildContext.Construct(premiumCustomerBuild);
			var premiumCustomer = premiumCustomerBuild.GetCustomer();

			await _shopContext.Customers.AddAsync(premiumCustomer);
			await _shopContext.SaveChangesAsync();
			return premiumCustomer;
		}

		public async Task<Customer> RegisterVIPCustomer(Customer customer)
		{
			var vipCustomerBuild = new VIPCustomerBuild(customer.Name, customer.Password, customer.Email);

			_buildContext.Construct(vipCustomerBuild);
			var vipCustomer = vipCustomerBuild.GetCustomer();

			await _shopContext.Customers.AddAsync(vipCustomer);
			await _shopContext.SaveChangesAsync();
			return vipCustomer;
		}
		public async Task<Customer> RegisterStandardCustomer(Customer customer)
		{
			var standardCustomerBuild = new StandardCustomerBuild(customer.Name, customer.Password, customer.Email);

			_buildContext.Construct(standardCustomerBuild);
			var standardCustomer = standardCustomerBuild.GetCustomer();

			await _shopContext.Customers.AddAsync(standardCustomer);
			await _shopContext.SaveChangesAsync();
			return standardCustomer;
		}

		public async Task<Customer> LoginCustomer(string email, string password)
		{
			var customer = await _shopContext.Customers
				.FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password));
			if (customer != null)
			{
				return customer;
			}

			return null;
		}

		public bool DeleteCustomer(int id)
		{
			var customer = _shopContext.Customers.FirstOrDefault(c => c.Id == id);
			if (customer is null)
			{
				return false;
			}

			_shopContext.Customers.Remove(customer);
			_shopContext.SaveChanges();

			return true;
		}

	}
}
