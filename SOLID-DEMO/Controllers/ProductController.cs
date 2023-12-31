﻿using Microsoft.AspNetCore.Mvc;
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
			var result = await _uow.ProductRepository.GetProducts();
			if (result.Count > 0)
			{
				return Ok(result);
			}

			return NotFound();
		}

		[HttpGet("/products/{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			var result = await _uow.ProductRepository.GetProduct(id);
			if (result != null)
			{
				return Ok(result);

			}
			return NotFound();
		}

		[HttpPost("/products")]
		public async Task<IActionResult> AddProduct(Product newProd)
		{
			var result = await _uow.ProductRepository.AddProduct(newProd);
			return Ok(result);
		}

	}
}
