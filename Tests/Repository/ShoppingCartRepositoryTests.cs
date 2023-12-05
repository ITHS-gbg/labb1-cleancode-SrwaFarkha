using Moq;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Repository
{
	public class ShoppingCartRepositoryTests
	{
		private Mock<IShoppingCartRepository> mockShoppingCartRepo;

		[SetUp]
		public void RunBeforeEveryTest()
		{
			mockShoppingCartRepo = new Mock<IShoppingCartRepository>(MockBehavior.Strict);
		}

		[Test]
		public async Task ShoppingCartRepository_AddProductToShoppingCart_ReturnsTrue()
		{
			//Arrange
			var order = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1 }
			};

			//Act
			mockShoppingCartRepo.Setup(x => x.AddProductToShoppingCart(It.IsAny<CustomerCart>())).Returns(Task.FromResult(true));
			var result = await mockShoppingCartRepo.Object.AddProductToShoppingCart(order);

			//Assert
			Assert.IsTrue(result);

		}

		[Test]
		public async Task ShoppingCartRepository_DeleteProductFromShoppingCart_ReturnTrue()
		{
			// Arrange
			var cart = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1 }
			};

			// Act
			mockShoppingCartRepo.Setup(x => x.DeleteProductFromShoppingCart(It.IsAny<CustomerCart>())).ReturnsAsync(true);
			var result = await mockShoppingCartRepo.Object.DeleteProductFromShoppingCart(cart);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
