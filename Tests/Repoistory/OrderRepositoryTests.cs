using Castle.Core.Resource;
using Moq;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using SOLIDDEMO.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Repoistory
{
	public class OrderRepositoryTests
	{
		private Mock<IOrderRepository> mockOrderRepo;

		[SetUp]
		public void RunBeforeEveryTest()
		{
			mockOrderRepo = new Mock<IOrderRepository>(MockBehavior.Strict);
		}

		[Test]
		public async Task OrderRepository_GetAllOrders_ReturnAllOrders()
		{
			// arrange
			var customer = new Customer
			{
				Id = 1,
				Name = "John Doe",
			};

			var orders = new List<Order>
			{
				new Order(1, new List<Product>
				{
					new Product(1,"Product1", "Black")
				}, customer, shippingDate:DateTime.Now),
			};

			// act
			mockOrderRepo.Setup(x => x.GetAllOrders()).Returns(Task.FromResult(orders));

			var result = await mockOrderRepo.Object.GetAllOrders();

			// assert
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task OrderRepository_GetOrdersForCustomer_ReturnOrdersFromCustomerById()
		{
			//Arrange 
			var customer = new Customer
			{
				Id = 1,
				Name = "John Doe",
			};

			var orders = new List<Order>
			{
				new Order(1, new List<Product>
				{
					new Product(1,"Product1", "Black")
				}, customer, shippingDate:DateTime.Now),
			};

			//Act
			mockOrderRepo.Setup(x => x.GetOrdersForCustomer(It.IsAny<int>())).Returns(Task.FromResult(orders));

			var result = await mockOrderRepo.Object.GetOrdersForCustomer(customer.Id);

			//Assert
			Assert.IsNotNull(result);

		}

		[Test]
		public async Task OrderRepository_PlaceOrder_ReturnsOrder()
		{
			//Arrange
			var customerCart = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1, 2, 3 }
			};
			//Act
			mockOrderRepo.Setup(x => x.PlaceOrder(It.IsAny<CustomerCart>())).Returns(Task.FromResult(true));
			var result = await mockOrderRepo.Object.PlaceOrder(customerCart);

			//Assert
			Assert.IsTrue(result);

		}

		[Test]
		public async Task OrderRepository_CancelOrder_ReturnsTrue()
		{
			// Arrange
			var orderId = 1;
			var mockOrderRepo = new Mock<IOrderRepository>();

			// Act
			mockOrderRepo.Setup(x => x.CancelOrder(It.IsAny<int>())).Returns(Task.FromResult(true));
			var result = await mockOrderRepo.Object.CancelOrder(orderId);

			// Assert
			Assert.IsTrue(result);

		}

		[Test]
		public async Task OrderRepository_AddProductToShoppingCart_ReturnsTrue()
		{
			//Arrange
			var order = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1 }
			};

			//Act
			mockOrderRepo.Setup(x => x.AddProductToShoppingCart(It.IsAny<CustomerCart>(),It.IsAny<int>())).Returns(Task.FromResult(true));
			var result = await mockOrderRepo.Object.AddProductToShoppingCart(order, 1);

			//Assert
			Assert.IsTrue(result);

		}

		[Test]
		public async Task OrderRepository_DeleteProductFromShoppingCart_ReturnTrue()
		{
			// Arrange
			var order = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1 }
			};

			// Act
			mockOrderRepo.Setup(x => x.DeleteProductFromShoppingCart(It.IsAny<CustomerCart>(), It.IsAny<int>())).ReturnsAsync(true);
			var result = await mockOrderRepo.Object.DeleteProductFromShoppingCart(order, 1);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
