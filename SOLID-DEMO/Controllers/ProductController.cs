using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Repositories.Interfaces;

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
	
	}
}
