using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;

namespace Server.Controllers
{
	[Route("api/shopping-cart")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IUnitOfWork _uow;

		public ShoppingCartController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpPatch("shopping-cart/add")]
		public async Task<IActionResult> AddProductToShoppingCart(CustomerCart itemsToAdd)
		{
			var itemAdded = await _uow.ShoppingCartRepository.AddProductToShoppingCart(itemsToAdd);

			if (itemAdded)
			{
				return Ok(itemAdded);
			}

			return NotFound();
		}

		[HttpPatch("shopping-cart/remove")]
		public async Task<IActionResult> DeleteProductFromShoppingCart(CustomerCart itemsToRemove)
		{
			var result = await _uow.ShoppingCartRepository.DeleteProductFromShoppingCart(itemsToRemove);
			if (result)
			{
				return Ok(result);

			}
			return BadRequest();
		}
	}
}
