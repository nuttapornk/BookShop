using BookShop.Infra;
using BookShop.WebUi.Mediator.Books.Commands.CreateBook;
using BookShop.WebUi.Mediator.Books.Queries.GetBooks;
using BookShop.WebUi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebUi.Controllers
{
    public class BooksController : MvcController
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        private readonly ISelectListService _selectList;

        public BooksController(ILogger logger, AppDbContext context, ISelectListService selectList)
        {
            _logger = logger;   
            _context = context;
            _selectList = selectList;
        }
        
        public async Task<IActionResult> Index(GetBooksQuery model)
        {
            ViewData["SortNameThai"] = model.SortExpresstion == "NameThai" ? "-NameThai" : "NameThai";
            

            var results = await Mediator.Send(model);
            return View(results);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Publisher"] = await _selectList.GetPublisherAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateBookCommand model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("");
                }

                await Mediator.Send(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
            }
            ViewData["Publisher"] = await _selectList.GetPublisherAsync(model.PublisherId);
            return View(model);
            
        }

    }
}
