using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Enums;

namespace Tests.Controllers
{
	public class OrderControllerTests
	{
		private Mock<IUnitOfWork> _unitOfWork;
		private OrderController _orderController;

		[SetUp]
		public void Setup()
		{
			_unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
			_orderController = new OrderController(_unitOfWork.Object);
		}

		[Test]
		[TestCase("John Doe", "111", "email@hotmail.com", 1, "Product1", "Black")]
		[TestCase( "Jane Smith",  "222", "jane@gmail.com", 2, "Product2", "White")]
		public void OrderController_GetAllOrders_ReturnsAllOrders(
			string customerName, string customerPassword, string customerEmail,
			int orderId, string productName, string description)
		{
			// Arrange
			var customer = new Customer(customerName, customerPassword, customerEmail, Enums.CustomerLevel.Premium);

			var orders = new List<Order>
			{
				new Order(orderId, new List<Product>
				{
					new Product(1, productName, description, 1)
				}, customer, shippingDate: DateTime.Now),
			};


			// Act 
			_unitOfWork.Setup(x => x.OrderRepository.GetAllOrders()).Returns(Task.FromResult(orders));

			var result = _orderController.GetAllOrders();

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		[TestCase( "John Doe", "email@hotmail.com", "111", 1, "Product1", "Black")]
		[TestCase( "John Doe", "email@hotmail.com", "111", 2, "Product2", "White")]
		public void OrderController_GetOrdersForCustomer_ReturnsOrderForCustomerById(
			string customerName, string customerEmail, string customerPassword,
			int orderId, string productName, string description)
		{
			//Arrange
			var customer = new Customer(customerName, customerPassword, customerEmail, Enums.CustomerLevel.Premium);


			var orders = new List<Order>
			{
				new Order(orderId, new List<Product>
				{
					new Product(1, productName, description, 1)
				}, customer, shippingDate: DateTime.Now),
			};

			//Act
			_unitOfWork.Setup(x => x.OrderRepository.GetOrdersForCustomer(It.IsAny<int>())).Returns(Task.FromResult(orders));
			var result = _orderController.GetOrdersForCustomer(1);

			//Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task OrderController_PlaceOrder_ReturnsTrue()
		{
			//Arrange
			var customerCart = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1, 2, 3 }
			};
			//Act
			_unitOfWork.Setup(x => x.OrderRepository.PlaceOrder(It.IsAny<CustomerCart>())).Returns(Task.FromResult(true));
			var result = await _orderController.PlaceOrder(customerCart) as OkObjectResult;

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(true, result.Value);
		}

		[Test]
		public void OrderController_CancelOrder_ReturnsTrue()
		{
			//arrange
			var orderId = 1;

			//act
			_unitOfWork.Setup(x => x.OrderRepository.CancelOrder(It.IsAny<int>())).Returns(Task.FromResult(true));
			var result =  _orderController.CancelOrder(orderId);

			// Assert
			_unitOfWork.Verify(uow => uow.OrderRepository.CancelOrder(orderId));

			Assert.IsTrue(result.IsCompleted);
		}

		


	}
}
