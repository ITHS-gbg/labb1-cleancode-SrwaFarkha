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
	public class ShoppingCartControllerTests
	{
		private Mock<IUnitOfWork> _unitOfWork;
		private ShoppingCartController _shoppingCartController;

		[SetUp]
		public void Setup()
		{
			_unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
			_shoppingCartController = new ShoppingCartController(_unitOfWork.Object);
		}

		[Test]
		public void ShoppingCartController_AddProductToShoppingCart_ReturnsTrue()
		{
			//Arrange
			var customerCart = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1, 2, 3 }
			};

			//Act
			_unitOfWork.Setup(x => x.ShoppingCartRepository.AddProductToShoppingCart(It.IsAny<CustomerCart>())).Returns(Task.FromResult(true));
			var result = _shoppingCartController.AddProductToShoppingCart(customerCart);

			//Assert
			Assert.IsTrue(result.IsCompleted);

		}

		[Test]
		public void ShoppingCartController_DeleteProductFromShoppingCart_ReturnsTrue()
		{
			// Arrange
			var customerCart = new CustomerCart
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1 }
			};

			//act
			_unitOfWork.Setup(x => x.ShoppingCartRepository.DeleteProductFromShoppingCart(It.IsAny<CustomerCart>())).Returns(Task.FromResult(true));
			var result = _shoppingCartController.DeleteProductFromShoppingCart(customerCart);

			//Assert
			Assert.IsTrue(result.IsCompleted);

		}
	}
}
