using Moq;
using Server.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controllers;
using Shared.Classes;
using Shared.Enums;
using Microsoft.AspNetCore.Mvc;

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
		[TestCase("Customer1", "customer1@gmail.com",   "111")]
		[TestCase("Customer2", "customer2@gmail.com",   "222")]

		public void CustomerController_GetCustomers_ReturnsAllCustomers(string name, string password, string email)
		{
			//Arrange
			var customers = new List<Customer>
			{
				new Customer (name, password, email, Enums.CustomerLevel.Premium)
			};

			_unitOfWork.Setup(x => x.CustomerRepository.GetAll()).Returns(Task.FromResult(customers));
			//Act
			var result = _customerController.GetCustomers();

			//Assert
			_unitOfWork.Verify(x => x.CustomerRepository.GetAll(), Times.Once);
			Assert.IsNotNull(result);

		}

		[Test]
		public async Task CustomerController_GetCustomer_ReturnCustomer()
		{
			// arrange
			var expectedCustomer = new Customer("Customer1", "Password1", "Email1@hotmail.com", Enums.CustomerLevel.Standard);

			// act
			_unitOfWork.Setup(x => x.CustomerRepository.GetByEmailAddress(It.IsAny<string>()))
				.Returns(Task.FromResult(expectedCustomer));
			var result = await _customerController.GetCustomer(expectedCustomer.Email);
			
			// assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<OkObjectResult>(result);

			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.IsInstanceOf<Customer>(okResult.Value);
		}


		[Test]
		public async Task CustomerController_RegisterCustomer_ReturnCustomer()
		{
			//Arrange
			var registerCustomer = new Customer("Customer1", "Password1", "Email1@hotmail.com", Enums.CustomerLevel.Standard);
			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.RegisterCustomer(It.IsAny<Customer>()))
				.Returns(Task.FromResult(registerCustomer));

			var result = await _customerController.RegisterCustomer(registerCustomer);

			// assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<OkObjectResult>(result);

			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);

			Assert.IsInstanceOf<Customer>(okResult.Value);

			var returnedCustomer = okResult.Value as Customer;
			Assert.IsNotNull(returnedCustomer);

			Assert.AreEqual(registerCustomer.Email, returnedCustomer.Email);
		}

		[Test]
		public async Task CustomerController_RegisterVIPCustomer_ReturnCustomer()
		{
			//Arrange 
			var vipRegisterCustomer =
				new Customer("Customer1", "Password1", "Email@gmail.com", Enums.CustomerLevel.VIP);
			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.RegisterVIPCustomer(It.IsAny<Customer>()))
				.Returns(Task.FromResult(vipRegisterCustomer));
			var controller = new CustomerController(_unitOfWork.Object);
			var result = await controller.RegisterVIPCustomer(vipRegisterCustomer) as ObjectResult;

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);

			var returnedCustomer = result.Value as Customer;
			Assert.IsNotNull(returnedCustomer);
			Assert.AreEqual(vipRegisterCustomer.Email, returnedCustomer.Email);
		}

		[Test]
		public async Task CustomerController_RegisterStandardCustomer_ReturnCustomer()
		{
			//Arrange
			var standardRegisterCustomer =
				new Customer("Customer1", "Password1", "Email@gmail.com", Enums.CustomerLevel.Standard);
			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.RegisterStandardCustomer(It.IsAny<Customer>()))
				.Returns(Task.FromResult(standardRegisterCustomer));
			var controller = new CustomerController(_unitOfWork.Object);
			var result = await controller.RegisterStandardCustomer(standardRegisterCustomer) as ObjectResult;

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);

			var returnedCustomer = result.Value as Customer;
			Assert.IsNotNull(returnedCustomer);
			Assert.AreEqual(standardRegisterCustomer.Email, returnedCustomer.Email);
		}

		[Test]
		public async Task CustomerController_RegisterPremiumCustomer_ReturnsCustomer()
		{
			//Arrange
			var premiumRegisterCustomer =
				new Customer("Customer1", "Password1", "Email@gmail.com", Enums.CustomerLevel.Premium);
			//Act
			_unitOfWork.Setup(x => x.CustomerRepository
				.RegisterPremiumCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(premiumRegisterCustomer));
			var controller = new CustomerController(_unitOfWork.Object);
			var result = await controller.RegisterPremiumCustomer(premiumRegisterCustomer) as ObjectResult;
			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);

			var returnedCustomer = result.Value as Customer;
			Assert.IsNotNull(returnedCustomer);
			Assert.AreEqual(premiumRegisterCustomer.Email, returnedCustomer.Email);
		}


		[Test]
		[TestCase("customer1@gmail.com", "111")]
		[TestCase("customer2@gmail.com", "222")]

		public async Task CustomerController_LoginCustomer_ReturnsCustomer(string email, string password)
		{
			//Arrange
			var customer = new Customer(email, password);

			//Act
			_unitOfWork.Setup(x => x.CustomerRepository.LoginCustomer(It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(customer);

			var result = await _customerController.LoginCustomer(email, password);

			//Assert
			Assert.NotNull(result);
			Assert.IsInstanceOf<OkObjectResult>(result);

			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);

			Assert.IsInstanceOf<Customer>(okResult.Value);

			var returnedCustomer = okResult.Value as Customer;
			Assert.IsNotNull(returnedCustomer);

			Assert.AreEqual(email, returnedCustomer.Email);
			Assert.AreEqual(password, returnedCustomer.Password);
		}

		[Test]
		[TestCase("customer1@gmail.com", 1, "Customer1", "111")]
		public async Task CustomerController_DeleteCustomer_ReturnsTrue(string email, int id, string name, string password)
		{
			// Arrange
			var customer = new Customer(id, name, password, email, Enums.CustomerLevel.Premium);

			// Act
			_unitOfWork.Setup(x => x.CustomerRepository.DeleteCustomer(It.IsAny<int>())).Returns(true);

			var result = await _customerController.DeleteCustomer(customer.Id);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<OkObjectResult>(result);

			var okResult = (OkObjectResult)result;
			Assert.AreEqual(true, okResult.Value);
		}
	}
}
