using Shared.Classes;

namespace Server.DataAccess.Repositories.Interfaces
{
	public interface ICustomerRepository
	{
		Task<List<Customer>> GetAll();
		Task<Customer> GetByEmailAddress(string email);
		Task<Customer> RegisterCustomer(Customer customer);
		Task<Customer> RegisterPremiumCustomer(Customer customer);
		Task<Customer> RegisterVIPCustomer(Customer customer);
		Task<Customer> RegisterStandardCustomer(Customer customer);
		Task<Customer> LoginCustomer(string email, string password);
		bool DeleteCustomer(int id);
	}
}
