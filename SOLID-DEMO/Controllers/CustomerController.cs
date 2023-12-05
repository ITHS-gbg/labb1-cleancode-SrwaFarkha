using Microsoft.AspNetCore.Mvc;
using Server.DataAccess;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Server.Controllers
{
	[Route("api/customer")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IUnitOfWork _uow;

		public CustomerController(IUnitOfWork uow)
		{
			_uow = uow;
		}


		[HttpGet("/customers")]
		public async Task<IActionResult> GetCustomers()
		{
			var result = _uow.CustomerRepository.GetAll();
			return Ok(result);
		}

		[HttpGet("/customers/{email}")]
		public async Task<IActionResult> GetCustomer(string email)
		{
			return Ok(await _uow.CustomerRepository.GetByEmailAddress(email));
		}

		[HttpPost("/customers/register")]
		public async Task<IActionResult> RegisterCustomer(Customer customer)
		{
			if (!customer.Email.Contains("@"))
				throw new ValidationException("Email is not an email");

			return Ok(await _uow.CustomerRepository.RegisterCustomer(customer));
		}

		[HttpPost("/customers/register/standard")]
		public async Task<IActionResult> RegisterStandardCustomer(Customer customer)
		{
			if (!customer.Email.Contains("@"))
				throw new ValidationException("Email is not an email");

			return Ok(await _uow.CustomerRepository.RegisterStandardCustomer(customer));
		}

		[HttpPost("/customers/register/premium")]
		public async Task<IActionResult> RegisterPremiumCustomer(Customer customer)
		{
			if (!customer.Email.Contains("@"))
				throw new ValidationException("Email is not an email");

			return Ok(await _uow.CustomerRepository.RegisterPremiumCustomer(customer));
		}


		[HttpPost("/customers/register/vip")]
		public async Task<IActionResult> RegisterVIPCustomer(Customer customer)
		{
			if (!customer.Email.Contains("@"))
				throw new ValidationException("Email is not an email");

			return Ok(await _uow.CustomerRepository.RegisterVIPCustomer(customer));
		}


		[HttpPost("/customers/login")]
		public async Task<IActionResult> LoginCustomer(string email, string password)
		{
			var customer = await _uow.CustomerRepository.LoginCustomer(email, password);
			if (customer is not null)
			{
				return Ok(customer);
			}
			return NotFound();

		}

		[HttpDelete("/customers/delete/{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var isDeleted = _uow.CustomerRepository.DeleteCustomer(id);
			if (isDeleted)
				return Ok(isDeleted);
			
			return BadRequest();

		}
	}
}
