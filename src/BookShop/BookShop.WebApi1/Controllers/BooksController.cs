using BookShop.Common;
using BookShop.WebApi1.Process.Books.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ApiController
    {
        public BooksController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get(string? name= "",string? isbn = "",decimal? price = 0,int page = 1,string? sortExpression = "Id")
        {
            try
            {
                GetBooksQuery req = new()
                {
                    Name = name,
                    Isbn = isbn,
                    Price = price,
                    Page = page,
                    SortExpresstion = string.IsNullOrEmpty(sortExpression) ? "Id" : sortExpression,
                };
                var result = await Mediator.Send(req);
                return Ok(new BaseResponse<GetBooksRes>
                {
                    Success = true,    
                    Result = result
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<dynamic>
                {
                    Success = false,
                    Result = null,
                    ErrorMessage = ex.Message
                });
            }

            
        }
    }
}
