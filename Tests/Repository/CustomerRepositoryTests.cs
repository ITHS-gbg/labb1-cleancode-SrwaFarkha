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
using Server.DataAccess.Builder;
using Shared.Enums;
using Server.DataAccess.Builder.Interface;

namespace Tests.Repository
{
	[TestFixture]
	public class CustomerRepositoryTests
	{

		private Mock<ICustomerRepository> mockCustomerRepo;
		private CustomerBuildContext buildCustomer;


		[SetUp]
		public void RunBeforeEveryTest()
		{
			mockCustomerRepo = new Mock<ICustomerRepository>(MockBehavior.Strict);
			buildCustomer = new CustomerBuildContext();
		}


		[Test]
		public async Task CustomerRepository_GetAll_ReturnAllCustomers()
		{
			// arrange
			var customers = new List<Customer>
			{
				new Customer("Customer1", "Password1", "Email1@hotmail.com", Enums.CustomerLevel.Premium),
				new Customer("Customer2", "Password2", "Email2@hotmail.com", Enums.CustomerLevel.Standard),
				new Customer("Customer3", "Password3", "Email3@hotmail.com", Enums.CustomerLevel.Standard),

			};

			// act
			mockCustomerRepo.Setup(x => x.GetAll()).Returns(Task.FromResult(customers));

			var result = await mockCustomerRepo.Object.GetAll();

			// assert
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task CustomerRepository_GetByEmailAddress_ReturnsCustomer()
		{
			// arrange
			var expectedCustomer = new Customer("Customer1", "Password1", "Email1@hotmail.com", Enums.CustomerLevel.VIP);

			// act
			mockCustomerRepo.Setup(x => x.GetByEmailAddress(It.IsAny<string>())).Returns(Task.FromResult(expectedCustomer));

			var result = await mockCustomerRepo.Object.GetByEmailAddress(expectedCustomer.Email);

			// assert
			mockCustomerRepo.Verify(x => x.GetByEmailAddress(expectedCustomer.Email), Times.Once);
			Assert.That(result.Email, Is.EqualTo(expectedCustomer.Email));
		}

		[Test]
		public async Task CustomerRepository_RegisterCustomer_ReturnsCustomer()
		{
			// arrange
			var registerCustomer = new Customer("Customer1", "Password1", "Email1@hotmail.com", Enums.CustomerLevel.Standard);

			mockCustomerRepo.Setup(x => x.RegisterCustomer(It.IsAny<Customer>()))
				.Returns(Task.FromResult(registerCustomer));

			// act
			var result = await mockCustomerRepo.Object.RegisterCustomer(registerCustomer);

			// assert
			Assert.NotNull(result);
		}

		[Test]
		public async Task CustomerRepository_LoginCustomer_ReturnsCustomer()
		{
			// arrange
			var loginCustomer = new Customer("Customer1", "Password1", "Email1@hotmail.com",
				Enums.CustomerLevel.Standard);

			// act
			mockCustomerRepo.Setup(x => x.LoginCustomer(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(loginCustomer));
			var result = await mockCustomerRepo.Object.LoginCustomer(loginCustomer.Email, loginCustomer.Password);

			// assert
			Assert.NotNull(result);
			Assert.AreEqual(loginCustomer.Email, result.Email);
			Assert.AreEqual(loginCustomer.Password, result.Password);
		}


		[Test]
		public void CustomerRepository_DeleteCustomer_ReturnsTrue()
		{
			// arrange
			var deleteCustomer = new Customer("Customer1", "Password1", "Email1@hotmail.com", Enums.CustomerLevel.Standard);

			// act
			mockCustomerRepo.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Returns(true);

			var result = mockCustomerRepo.Object.DeleteCustomer(deleteCustomer.Id);

			// assert
			Assert.IsTrue(result);
		}


		[Test]
		public void PremiumCustomerBuild_ApplyDiscount_ReturnsDiscount()
		{
			//Arrange
			var customerBuilder = new PremiumCustomerBuild( "customer1", "password1","email1@hotmal.com");

			//act
			var expectedDiscountRate = 15;
			customerBuilder.SetDiscountRate(expectedDiscountRate);
			var discountRate = customerBuilder.GetDiscountRate();

			//assert
			Assert.AreEqual(expectedDiscountRate, discountRate);
		}


		[Test]
		public void PremiumCustomerBuild_RegisterPremiumCustomer_ReturnsPremiumCustomer()
		{
			//Arrange
			var premiumCustomerBuild = new PremiumCustomerBuild("customer1", "password1", "email1@hotmal.com");
			
			//act
			premiumCustomerBuild.Build();


			buildCustomer.Construct(premiumCustomerBuild);
			var premiumCustomer = premiumCustomerBuild.GetCustomer();

			mockCustomerRepo.Setup(x => x.RegisterPremiumCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(premiumCustomer));

			//assert
			Assert.AreEqual(premiumCustomer.Level, Enums.CustomerLevel.Premium);
		}

	
		[Test]
		public void VIPCustomerBuild_ApplyDiscount_ReturnsDiscount()
		{
			//Arrange
			var customer1 = new VIPCustomerBuild( "customer1", "password1", "email1@hotmal.com");
			buildCustomer.Construct(customer1);

			var expectedDiscountRate = 20;
			customer1.Build();
			customer1.SetDiscountRate(expectedDiscountRate);
			//act
			var discountRate = customer1.GetDiscountRate();

			//assert

			Assert.AreEqual(expectedDiscountRate, discountRate);

		}

		[Test]
		public void VIPCustomerBuild_RegisterVIPCustomer_ReturnsVIPCustomer()
		{
			//Arrange
			var vipCustomerBuild = new VIPCustomerBuild("customer1", "password1", "email1@hotmal.com");
			vipCustomerBuild.Build();
			buildCustomer.Construct(vipCustomerBuild);
			var vipCustomer = vipCustomerBuild.GetCustomer();

			//act
			mockCustomerRepo.Setup(x => x.RegisterVIPCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(vipCustomer));

			//assert
			Assert.AreEqual(Enums.CustomerLevel.VIP, vipCustomer.Level);

		}

		[Test]
		public void StandardCustomerBuild_ApplyDiscount_ReturnsDiscount()
		{
			//Arrange
			var customer1 = new StandardCustomerBuild("customer1", "password1", "email1@hotmal.com");
			buildCustomer.Construct(customer1);

			var expectedDiscountRate = 10;
			customer1.Build();
			customer1.SetDiscountRate(expectedDiscountRate);
			//act
			var discountRate = customer1.GetDiscountRate();

			//assert
			Assert.AreEqual(expectedDiscountRate, discountRate);
		}

		[Test]
		public void StandardCustomerBuild_RegisterStandardCustomer_ReturnsStandardCustomer()
		{
			//Arrange
			var standardCustomerBuild = new StandardCustomerBuild("customer1", "password1", "email1@hotmal.com");
			standardCustomerBuild.Build();
			buildCustomer.Construct(standardCustomerBuild);
			var standardCustomer = standardCustomerBuild.GetCustomer();

			//act
			mockCustomerRepo.Setup(x => x.RegisterStandardCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(standardCustomer));

			//assert
			Assert.AreEqual( Enums.CustomerLevel.Standard, standardCustomer.Level);
		}
	}
}