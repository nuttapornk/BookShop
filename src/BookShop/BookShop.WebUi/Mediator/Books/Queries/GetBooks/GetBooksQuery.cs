using BookShop.Infra;
using BookShop.WebUi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;

namespace BookShop.WebUi.Mediator.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IEnumerable<GetBooksRes>>
    {
        public int Page { get; set; }
        public string Name { get; set; }
        public int? PublisherId { get; set; }
        public string Author { get; set; }
        public string SortExpresstion { get; set; }

        public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<GetBooksRes>>
        {
            private readonly AppDbContext _context;
            private readonly AppSetting _appSetting;
            public GetBooksQueryHandler(AppDbContext context,IOptions<AppSetting> appSetting)
            {
                _context = context;
                _appSetting = appSetting.Value;
            }

            public async Task<IEnumerable<GetBooksRes>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
            {
                var q1 = _context.Books
                    .Include(a => a.Publisher)
                    .Select(a => new GetBooksRes
                    {
                        Id = a.Id,
                        NameThai = a.NameThai,
                        NameEng = a.NameEng,
                        Isbn = a.Isbn,
                        CoverPrice = a.CoverPrice,
                        Publisher = a.Publisher
                    })
                    .AsNoTracking()
                    .AsQueryable();

                var data = await PagingList.CreateAsync(q1, _appSetting.PageSize, request.Page, request.SortExpresstion, "Id");
                data.Action = "Index";
                data.PageParameterName = "Page";
                data.RouteValue = new RouteValueDictionary
                {
                    {"Name",request.Name },
                    {"PublisherId",request.PublisherId },
                    {"Author",request.Author },
                    {"SortExpression",request.SortExpresstion }
                };
                return data;

            }
        }
    }
}
