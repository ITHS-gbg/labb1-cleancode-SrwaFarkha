using Moq;
using Server.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controllers;
using Shared.Classes;

namespace Tests.Controllers
{
	[TestFixture]

	public class CustomerControllerTests
	{
		private Mock<IUnitOfWork> _unitOfWork;
		private CustomerController _customerController;

		[SetUp]
		public void Setup()
		{
			_unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
			_customerController = new CustomerController(_unitOfWork.Object);
		}

		[Test]
		[TestCase("customer1@gmail.com", 1, "Customer1", "111")]
		[TestCase("customer2@gmail.com", 2, "Customer2", "222")]

		public void CustomerController_GetCustomers_ReturnsAllCustomers(string email, int id, string name, string password)
		{
			//Arrange
			var customers = new List<Customer>
			{
				new Customer { Email = email, Id = id, Name = name, Password = password }
			};

			_unitOfWork.Setup(uow => uow.CustomerRepository.GetAll()).Returns(Task.FromResult(customers));
			//Act
			var result = _customerController.GetCustomers();

			//Assert
			_unitOfWork.Verify(uow => uow.CustomerRepository.GetAll());
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task CustomerController_GetCustomer_ReturnCustomer()
		{
			// arrange
			var expectedCustomer = new Customer(1, "Customer1", "Password1", "Email1@hotmail.com");

			// act
			_unitOfWork.Setup(x => x.CustomerRepository.GetByEmailAddress(It.IsAny<string>())).Returns(Task.FromResult(expectedCustomer));

			var result = await _customerController.GetCustomer(expectedCustomer.Email);
			
			// assert
			//_unitOfWork.Verify(x => x.CustomerRepository.GetByEmailAddress(expectedCustomer.Email));

			Assert.IsNotNull(result);
			
		}

		[Test]
		public async Task CustomerController_RegisterUser_ReturnTrue()
		{
			//Arrange
			var registerCustomer = new Customer(1, "Customer1", "Password1", "Email1@hotmail.com");
			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.RegisterCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(registerCustomer));

			var result = await _customerController.RegisterUser(registerCustomer);

			// assert
			Assert.NotNull(result);

		}


		[Test]
		[TestCase("customer1@gmail.com", 1, "Customer1", "111")]
		[TestCase("customer2@gmail.com", 2, "Customer2", "222")]

		public async Task CustomerController_LoginCustomer_ReturnsTrue(string email, int id, string name, string password)
		{
			//Arrange
			var customer = new Customer
			{
				 Email = email, Id = id, Name = name, Password = password 
			};

			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.LoginCustomer(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(customer);

			var result = await _customerController.LoginCustomer(email, password);

			//Assert
			Assert.NotNull(result);

		}

		[Test]
		[TestCase("customer1@gmail.com", 1, "Customer1", "111")]
		public async Task CustomerController_DeleteCustomer_ReturnsTrue(string email, int id, string name, string password)
		{
			//Arrange
			var customer = new Customer
			{
				Email = email,
				Id = id,
				Name = name,
				Password = password
			};

			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.DeleteCustomer( It.IsAny<int>())).Returns(true);

			var result = await _customerController.DeleteCustomer(customer.Id);

			//Assert
			Assert.NotNull(result);

		}
	}
}
