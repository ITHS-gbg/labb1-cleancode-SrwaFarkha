using Microsoft.AspNetCore.Mvc;
using Server.DataAccess;
using Server.DataAccess.Repositories.Interfaces;
using Shared.Classes;

namespace Server.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IUnitOfWork _uow;

		public ProductController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpGet("/products")]
		public async Task<IActionResult> GetProducts()
		{
			var result = _uow.ProductRepository.GetProducts();
			return Ok(result);
		}

		[HttpGet("/products/{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			return Ok(_uow.ProductRepository.GetProduct);

		}

		[HttpPost("/products")]
		public async Task<IActionResult> AddProduct(Product newProd)
		{
			await _uow.ProductRepository.AddProduct(newProd);
			return Ok();
		}

	}
}
