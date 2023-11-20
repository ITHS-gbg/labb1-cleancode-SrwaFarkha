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
			return Ok(await _uow.CustomerRepository.GetAll());
		}

		[HttpGet("/customers/{email}")]
		public async Task<IActionResult> GetCustomer(string email)
		{
			return Ok(await _uow.CustomerRepository.GetByEmailAddress(email));
		}

		[HttpPost("/customers/register")]
		public async Task<IActionResult> RegisterUser(Customer customer)
		{
			if (!customer.Email.Contains("@"))
				throw new ValidationException("Email is not an email");

			return Ok(await _uow.CustomerRepository.RegisterCustomer(customer));
		}
	}
}
