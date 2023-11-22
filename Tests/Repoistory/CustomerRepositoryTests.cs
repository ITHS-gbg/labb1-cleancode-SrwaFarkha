using NUnit.Framework;
using Shared.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.DataAccess.Repositories;
using Tests.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Server.DataAccess;
using Server.DataAccess.Repositories.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using Castle.Core.Resource;

namespace Tests.Repository
{
	[TestFixture]
	public class CustomerRepositoryTests
	{

		private Mock<ICustomerRepository> mockCustomerRepo;


		[SetUp]
		public void RunBeforeEveryTest()
		{
			mockCustomerRepo = new Mock<ICustomerRepository>(MockBehavior.Strict);
		}


		[Test]
		public async Task GetAll_ReturnAllCustomers()
		{
			// arrange
			var customers = new List<Customer>
			{
				new Customer(1,"Customer1", "Password1", "Email1@hotmail.com"),
				new Customer(2,"Customer2", "Password2", "Email2@hotmail.com"),
				new Customer(3,"Customer3", "Password3", "Email3@hotmail.com"),
			};

			// act
			mockCustomerRepo.Setup(x => x.GetAll()).Returns(Task.FromResult(customers));

			var result = await mockCustomerRepo.Object.GetAll();

			// assert
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task GetByEmailAddress_ReturnsCustomer()
		{
			// arrange
			var expectedCustomer = new Customer(1,"Customer1", "Password1", "Email1@hotmail.com");

			// act
			mockCustomerRepo.Setup(x => x.GetByEmailAddress(It.IsAny<string>())).Returns(Task.FromResult(expectedCustomer));

			var result = await mockCustomerRepo.Object.GetByEmailAddress(expectedCustomer.Email);

			// assert
			Assert.AreEqual(result.Email, expectedCustomer.Email);
		}

		[Test]
		public async Task RegisterCustomer_ReturnsCustomer()
		{
			// arrange
			var registerCustomer = new Customer(1,"Customer1", "Password1", "Email1@hotmail.com");

			// act
			mockCustomerRepo.Setup(x => x.RegisterCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(registerCustomer));

			var result = await mockCustomerRepo.Object.RegisterCustomer(registerCustomer);

			// assert
			Assert.NotNull(result);
		}

		[Test]
		public async Task LoginCustomer_ReturnsCustomer()
		{
			// arrange
			var loginCustomer = new Customer(1,"Customer1", "Password1", "Email1@hotmail.com");

			// act
			mockCustomerRepo.Setup(x => x.LoginCustomer(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(loginCustomer));

			var result = await mockCustomerRepo.Object.LoginCustomer(loginCustomer.Email, loginCustomer.Password);

			// assert
			Assert.NotNull(result);
		}


		[Test]
		public async Task DeleteCustomer_ReturnsTrue()
		{
			// arrange
			var deleteCustomer = new Customer(1,"Customer1", "Password1", "Email1@hotmail.com");

			// act
			mockCustomerRepo.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Returns(true);

			var result = mockCustomerRepo.Object.DeleteCustomer(deleteCustomer.Id);

			// assert
			Assert.IsTrue(result);
		}






	}
}