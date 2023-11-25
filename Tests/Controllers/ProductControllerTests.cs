using Castle.Core.Resource;
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
	[TestFixture]
	public class ProductControllerTests
	{
		private Mock<IUnitOfWork> _unitOfWork;
		private ProductController _productController;

		[SetUp]
		public void Setup()
		{
			_unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
			_productController = new ProductController(_unitOfWork.Object);
		}

		[Test]
		[TestCase(1, "customer1", "black")]
		[TestCase(2, "customer2", "white")]

		public void ProductController_GetProducts_ReturnsAllProducts(int id, string name, string description)
		{
			//Arrange
			var products = new List<Product>
			{
				new Product(id,  name, description )
			};

			_unitOfWork.Setup(x => x.ProductRepository.GetProducts()).Returns(Task.FromResult(products));

			//Act
			var result = _productController.GetProducts();

			//Assert
			//_unitOfWork.Verify(x => x.ProductRepository.GetProducts());
			Assert.IsNotNull(result);
		}

		[Test]
		[TestCase(1, "Product1", "Blue")]
		[TestCase(2, "Product2", "Big")]

		public void ProductController_GetProduct_ReturnsProductById(int id, string name, string description)
		{
			//Arrange
			var product = new Product(id, name, description);

			//Act
			_unitOfWork.Setup(x => x.ProductRepository.GetProduct(It.IsAny<int>())).Returns(Task.FromResult(product));
			var result =  _productController.GetProduct(product.Id);

			//Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void ProductController_AddProduct_ReturnsNewProduct()
		{
			//Arrange
			var addProduct = new Product(4, "Product4", "Pink");

			//Act
			_unitOfWork.Setup(x => x.ProductRepository.AddProduct(It.IsAny<Product>())).Returns(Task.FromResult(addProduct));
			var result =  _productController.AddProduct(addProduct);

			//Assert
			Assert.NotNull(result);
		}
	}
}
