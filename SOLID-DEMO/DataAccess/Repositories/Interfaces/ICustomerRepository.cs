using Shared.Classes;

namespace Server.DataAccess.Repositories.Interfaces
{
	public interface ICustomerRepository
	{
		Task<List<Customer>> GetAll();
		Task<Customer> GetByEmailAddress(string email);
		Task<Customer> RegisterCustomer(Customer customer);
	}
}
