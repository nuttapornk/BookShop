using BookShop.Infra;
using BookShop.Common;
using MediatR;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Microsoft.VisualBasic;

namespace BookShop.WebApi1.Process.Books.Queries
{
    public class GetBooksQuery : IRequest<GetBooksRes>
    {
        public int Page { get; set; } = 1;
        public string? Name { get; set; } = string.Empty;
        public string? Isbn { get; set; } = string.Empty;
        public decimal? Price { get; set; } = null;
        public bool? Status { get; set; } = null;
        public string SortExpresstion { get; set; } = "Id";

        public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksRes>
        {
            private readonly AppDbContext _context;
            private readonly AppSetting _appSetting;
            public GetBooksQueryHandler(AppDbContext context,IOptions<AppSetting> appSetting)
            {
                _context = context;
                _appSetting = appSetting.Value;
            }

            public async Task<GetBooksRes> Handle(GetBooksQuery request, CancellationToken cancellationToken)
            {
                var q1 = _context.Books
                    .Select(a => new GetBooksResData
                    {
                        Id = a.Id,
                        Name = a.NameThai,
                        Isbn = a.Isbn,
                        Price = a.CoverPrice,
                        Status = a.Status == 1
                    }).AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    q1 = q1.Where(a => a.Name.Contains(request.Name));
                }

                if (!string.IsNullOrEmpty(request.Isbn))
                {
                    q1 = q1.Where(a=>a.Isbn.Contains(request.Isbn));
                }

                if (request.Price != null && request.Price > 0)
                {
                    q1 = q1.Where(a => a.Price == request.Price);
                }

                if (request.Status != null)
                {
                    q1 = q1.Where(a => a.Status == request.Status);
                }

                var data = await PagingList.CreateAsync(q1, _appSetting.PageSize, request.Page, request.SortExpresstion, "Id");

                GetBooksRes result = new()
                {
                    PageCount = data.PageCount,
                    PageIndex = data.PageIndex,
                    Data = data
                };

                return result;

            }
        }
    }
}
