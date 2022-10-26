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
        public int Page { get; set; } = 1;
        public string NameThai { get; set; }
        public string NameEng { get; set; }
        public string Isbn { get; set; }
        public int? PublisherId { get; set; }
        public string Author { get; set; }
        public string SortExpresstion { get; set; } = "Id";

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
                        Author = a.Author,
                        Publisher = a.Publisher
                    })
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.NameThai))
                {
                    q1 = q1.Where(a => a.NameThai.Contains(request.NameThai));
                }

                if (!string.IsNullOrEmpty(request.NameEng))
                {
                    q1 = q1.Where(a => a.NameEng.Contains(request.NameEng));
                }

                if (!string.IsNullOrEmpty(request.Isbn))
                {
                    q1 = q1.Where(a => a.Isbn.Contains(request.Isbn));
                }

                if (!string.IsNullOrEmpty(request.Author))
                {
                    q1 = q1.Where(a => a.Author.Contains(request.Author));
                }

                if (request.PublisherId != null)
                {
                    q1 = q1.Where(a => a.Publisher.Id == request.PublisherId);
                }

                var data = await PagingList.CreateAsync(q1, _appSetting.PageSize, request.Page, request.SortExpresstion, "Id");
                data.Action = "Index";
                data.PageParameterName = "Page";
                data.RouteValue = new RouteValueDictionary
                {
                    {"NameThai",request.NameThai },
                    {"NameEng",request.NameEng },
                    {"PublisherId",request.PublisherId },
                    {"Author",request.Author },
                    {"SortExpression",request.SortExpresstion }
                };
                return data;

            }
        }
    }
}
