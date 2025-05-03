using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    // Marks this class as a controller for API requests
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        // Private field to hold the IMediator instance
        // This is used to send requests to the MediatR library
        private IMediator? _mediator;

        // Protected property to access the IMediator instance 
        // If the meditator instance is not already created, it will be created here
        // If the instance is not found in the service collection, an exception will be thrown
        protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("Mediator not found in the service collection.");

        protected ActionResult<T> HandleResult<T>(Result<T> result)
        {
            if (!result.IsSuccess && result.Code == 404) return NotFound();

            if (result.IsSuccess && result.Value != null) return result.Value;

            return BadRequest(result.Error);
        }

    }
}
