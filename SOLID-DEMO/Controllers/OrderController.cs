using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Repositories.Interfaces;

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
		
	}
}
