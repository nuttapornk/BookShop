using BookShop.WebUi.Mediator.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebUi.Controllers
{
    public class BooksController : MvcController
    {
        public async Task<IActionResult> Index(GetBooksQuery model)
        {
            ViewData["SortNameThai"] = model.SortExpresstion == "NameThai" ? "-NameThai" : "NameThai";


            var results = await Mediator.Send(model);
            return View(results);
        }
    }
}
