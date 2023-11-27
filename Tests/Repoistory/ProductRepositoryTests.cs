using Moq;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Repoistory
{
	[TestFixture]
	public class ProductRepositoryTests
	{
		private Mock<IProductRepository> mockProductRepo;


		[SetUp]
		public void RunBeforeEveryTest()
		{
			mockProductRepo = new Mock<IProductRepository>(MockBehavior.Strict);
		}

		[Test]
		public void ProductRepository_GetProducts_ReturnsAllProducts()
		{
			// arrange
			var products = new List<Product>
			{
				new Product(1,"Product1", "Black", 1),
				new Product(2,"Product2", "White", 2),
				new Product(3,"Product3", "Big blue", 3),
			};

			// act
			mockProductRepo.Setup(x => x.GetProducts()).Returns(Task.FromResult(products));

			var result = mockProductRepo.Object.GetProducts();

			// assert
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task ProductRepository_GetProduct_ReturnsProduct()
		{
			// arrange
			var expectedProduct = new Product(1, "Product1", "Black", 1);

			// act
			mockProductRepo.Setup(x => x.GetProduct(It.IsAny<int>())).Returns(Task.FromResult(expectedProduct));

			var result = await mockProductRepo.Object.GetProduct(expectedProduct.Id);

			// assert
			Assert.AreEqual(result.Id, expectedProduct.Id);
		}

		[Test]
		public async Task ProductRepository_AddProduct_ReturnsNewProduct()
		{
			// arrange
			var addProduct = new Product(4, "Product4", "Yellow", 4);

			// act
			mockProductRepo.Setup(x => x.AddProduct(It.IsAny<Product>())).Returns(Task.FromResult(addProduct));

			var result = await mockProductRepo.Object.AddProduct(addProduct);

			// assert
			Assert.NotNull(result);
		}
	}
}
