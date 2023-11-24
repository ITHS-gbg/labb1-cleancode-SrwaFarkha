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
			return Ok(orders);
		}

		[HttpGet("/orders/customer/{id}")]
		public async Task<IActionResult> GetOrdersForCustomer(int id)
		{
			var getOrdersByCustomerId = await _uow.OrderRepository.GetOrdersForCustomer(id);

			if (getOrdersByCustomerId.Count == 0)
			{
				return NotFound($"No orders found for customer with ID {id}");
			}

			return Ok(getOrdersByCustomerId);
		}

		[HttpPost("/orders")]
		public async Task<IActionResult> PlaceOrder(CustomerCart cart)
		{
			var result = await _uow.OrderRepository.PlaceOrder(cart);

			if (result == true)
			{
				return Ok("Order placed successfully.");
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

			return Ok();
		}

		[HttpPatch("order/add/{id}")]
		public async Task<IActionResult> AddProductToShoppingCart(CustomerCart itemsToAdd, int id)
		{
			var itemAdded = await _uow.OrderRepository.AddProductToShoppingCart(itemsToAdd, id);

			if (itemAdded)
			{
				return Ok();
			}

			return NotFound();
		}

		[HttpPatch("order/remove/{id}")]
		public async Task<IActionResult> DeleteProductFromShoppingCart(CustomerCart itemsToRemove, int id)
		{
			var customer = await _uow.OrderRepository.DeleteProductFromShoppingCart(itemsToRemove, id);
			if (customer is false)
			{
				return BadRequest();
			}
			return Ok();
		}

	}
}
