using CookBook.Infrastructure.Responses;
using CookBook.Infrastructure.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers
{
    public class ApplicationController : Controller
    {
        protected readonly IMediator Mediator;

        protected ApplicationController(IMediator mediator)
        {
            Require.Objects.NotNull(mediator, nameof(mediator));
            
            Mediator = mediator;
        }
        protected ActionResult<T> Item<T>(T source)
        {
            if (source == null)
            {
                return NotFound();
            }

            return Ok(BuildContentApiResponse(source));
        }

        private ContentApiResponse<T> BuildContentApiResponse<T>(T source)
        {
            return new ContentApiResponse<T>
            {
                Content = source
            };
        }

    }
}