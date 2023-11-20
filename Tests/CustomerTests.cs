using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Classes;

namespace Tests
{
	[TestFixture]

	public class CustomerTests
	{

		[Test]
		public void CustomerTests_GetAllCustomers_Returns_Customers()
		{
			//arrange
			var customers = new List<Customer>();
			//act
			customers = GetCustomers();

			//assert
		
			Assert.IsNotEmpty(customers);
		}


		[Test]
		[TestCase("email1@hotmail.com")]
		public void CustomerTests_GetCustomerByEmail_Returns_Customer(string email)
		{
			//arrange
			var customer = new Customer();
			//act
			customer = GetCustomerByEmail(email);

			//assert

			Assert.AreEqual(customer.Email, email);
		}

		[Test]
		[TestCaseSource(nameof(CustomerTestCases))]
		public void CustomerTests_RegisterCustomer_Returns_True(Customer customer)
		{
			//act
			var customerRegister = RegisterCustomer(customer);

			//assert
			Assert.IsTrue(customerRegister);
		}

		private List<Customer> GetCustomers()
		{
			var customers = new List<Customer>();
			for (int i = 1; i < 5; i++)
			{
				var customer = new Customer($"Customer{i}", $"Password{i}", $"email{i}@hotmail.com");
				customers.Add(customer);
			}
			return customers;
		}

		private Customer GetCustomerByEmail(string email)
		{
			var customers = new List<Customer>();
			for (int i = 1; i < 5; i++)
			{
				var customer = new Customer($"Customer{i}", $"Password{i}", $"email{i}@hotmail.com");
				customers.Add(customer);
			}

			var customerToReturn = customers.FirstOrDefault(x => x.Email == email);
			return customerToReturn;
		}

		private bool RegisterCustomer(Customer customer)
		{
			if (customer != null)
			{
				return true;
			}
			return false;
		}





		private static IEnumerable<Customer> CustomerTestCases()
		{
			yield return new Customer("Customer1", "password", "email1@hotmail.com");
		}
	}
}
