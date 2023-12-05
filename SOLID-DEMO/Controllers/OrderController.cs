using Microsoft.AspNetCore.Mvc;
using Server.DataAccess;
using Server.DataAccess.Repositories;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;

namespace Server.Controllers
{
	[Route("api/order")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IUnitOfWork _uow;

		public OrderController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpGet("/orders")]
		public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _uow.OrderRepository.GetAllOrders();
			if (orders.Count > 0)
			{
				return Ok(orders);
			}

			return NotFound();
		}

		[HttpGet("/orders/customer/{id}")]
		public async Task<IActionResult> GetOrdersForCustomer(int id)
		{
			var customerOrders = await _uow.OrderRepository.GetOrdersForCustomer(id);

			if (customerOrders.Count == 0)
			{
				return NotFound($"No orders found for customer with ID {id}");
			}

			return Ok(customerOrders);
		}

		[HttpPost("/orders")]
		public async Task<IActionResult> PlaceOrder(CustomerCart cart)
		{
			var result = await _uow.OrderRepository.PlaceOrder(cart);

			if (result)
			{
				return Ok(result);
			}
		
			return BadRequest();
		}

		[HttpDelete("/orders/{id}")]
		public async Task<IActionResult> CancelOrder(int id)
		{
			var orderId = await _uow.OrderRepository.CancelOrder(id);
			if (orderId is false)
			{
				return NotFound();
			}
			return Ok(orderId);
		}

	}
}
