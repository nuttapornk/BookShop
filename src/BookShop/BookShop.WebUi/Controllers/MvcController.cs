using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebUi.Controllers
{
    public abstract class MvcController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    }
}
