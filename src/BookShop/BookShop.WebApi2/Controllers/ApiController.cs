using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebApi2.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
