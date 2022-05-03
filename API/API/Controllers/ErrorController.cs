using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Exceptions;
using System;

namespace OnlineAuction.API.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[ApiController]
	public class ErrorController : ControllerBase
	{
		[Route("error")]
		public IActionResult Error()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var exception = context.Error;

			return exception switch
			{
				UnauthorizedAccessException _ => Unauthorized(exception.Message),
				ArgumentException _ => BadRequest(exception.Message),
				UserIsAlreadyExistsException _ => BadRequest(exception.Message),
				_ => StatusCode(500)
			};
		}
	}
}
