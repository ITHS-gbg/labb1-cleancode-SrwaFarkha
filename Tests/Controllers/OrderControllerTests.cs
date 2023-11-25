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
		[TestCase(1, "John Doe", "email@hotmail.com", "111", 1, "Product1", "Black")]
		[TestCase(2, "Jane Smith", "jane@gmail.com", "222", 2, "Product2", "White")]
		public void OrderController_GetAllOrders_ReturnsAllOrders(
			int customerId, string customerName, string customerEmail, string customerPassword,
			int orderId, string productName, string description)
		{
			// Arrange
			var customer = new Customer
			{
				Id = customerId,
				Name = customerName,
				Email = customerEmail,
				Password = customerPassword
			};

			var orders = new List<Order>
			{
				new Order(orderId, new List<Product>
				{
					new Product(1, productName, description)
				}, customer, shippingDate: DateTime.Now),
			};


			// Act 
			_unitOfWork.Setup(x => x.OrderRepository.GetAllOrders()).Returns(Task.FromResult(orders));

			var result = _orderController.GetAllOrders();

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		[TestCase(1, "John Doe", "email@hotmail.com", "111", 1, "Product1", "Black")]
		[TestCase(1, "John Doe", "email@hotmail.com", "111", 2, "Product2", "White")]
		public void OrderController_GetOrdersForCustomer_ReturnsOrderForCustomerById(
			int customerId, string customerName, string customerEmail, string customerPassword,
			int orderId, string productName, string description)
		{
			//Arrange
			var customer = new Customer
			{
				Id = customerId,
				Name = customerName,
				Email = customerEmail,
				Password = customerPassword
			};

			var orders = new List<Order>
			{
				new Order(orderId, new List<Product>
				{
					new Product(1, productName, description)
				}, customer, shippingDate: DateTime.Now),
			};

			//Act
			_unitOfWork.Setup(x => x.OrderRepository.GetOrdersForCustomer(It.IsAny<int>())).Returns(Task.FromResult(orders));
			var result = _orderController.GetOrdersForCustomer(customerId);

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

		[Test]
		public void OrderController_AddProductToShoppingCart_ReturnsTrue()
		{
			//Arrange
			var customerCart = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1, 2, 3 }
			};

			//Act
			_unitOfWork.Setup(x => x.OrderRepository.AddProductToShoppingCart(It.IsAny<CustomerCart>(), It.IsAny<int>())).Returns(Task.FromResult(true));
			var result = _orderController.CancelOrder(customerCart.CustomerId);

			//Assert
			Assert.IsTrue(result.IsCompleted);

		}

		[Test]
		public void OrderController_DeleteProductFromShoppingCart_ReturnsTrue()
		{
			// Arrange
			var order = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1 }
			};

			//act
			_unitOfWork.Setup(x => x.OrderRepository.DeleteProductFromShoppingCart(It.IsAny<CustomerCart>(), It.IsAny<int>())).Returns(Task.FromResult(true));
			var result = _orderController.DeleteProductFromShoppingCart(order, 1);

			//Assert
			Assert.IsTrue(result.IsCompleted);

		}


	}
}
