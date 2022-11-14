using Azure.Core;
using BookShop.Common;
using Dapper;
using MediatR;
using Microsoft.Extensions.Options;

namespace BookShop.WebApi2.Process.Books.Queries
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
            private readonly DapperContext _context;
            private readonly AppSetting _appSetting;
            public GetBooksQueryHandler(DapperContext context,IOptions<AppSetting> appSetting)
            {
                _context = context;
                _appSetting = appSetting.Value;
            }

            public async Task<GetBooksRes> Handle(GetBooksQuery request, CancellationToken cancellationToken)
            {
                GetBooksRes result = new();
                var pageCount = await GetPageCountAsync(request);
                if (pageCount > 0)
                {
                    result = new GetBooksRes
                    {
                        PageCount = Convert.ToInt32(pageCount),
                        PageIndex = request.Page,
                        Data = await GetDataAsync(request)
                    };
                }
                
                return result;
            }

            private async Task<decimal> GetPageCountAsync(GetBooksQuery request)
            {
                var query = "select count(id) from Book where 1=1";
                query += GetParamString(request);
                using var connections = _context.CreateConnection();    
                var pageCount = await connections.QuerySingleOrDefaultAsync<decimal>(query,GetParamValue(request));
                return pageCount >0 ? Math.Ceiling( pageCount/_appSetting.PageSize) : 0;
            }

            private async Task<List<GetBooksResData>> GetDataAsync( GetBooksQuery request)
            {
                var query = "SELECT * FROM Book where 1=1";
                query += GetParamString(request);
                query += " ORDER BY Id";
                query += $" OFFSET {(request.Page - 1)*_appSetting.PageSize} rows fetch next {_appSetting.PageSize} rows only";

                using var connection = _context.CreateConnection();
                var books = await connection.QueryAsync<Infra.Models.Book>(query, GetParamValue(request));
                    
                return books.Select(a => new GetBooksResData
                {
                    Id = a.Id,
                    Isbn = a.Isbn,
                    Name = a.NameThai,
                    Price = a.CoverPrice,
                    Status = a.Status == 1
                }).ToList();
            
            }

            private static string GetParamString(GetBooksQuery request)
            {
                string query = "";
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query += $" and NameThai like @name";
                }

                if (!string.IsNullOrEmpty(request.Isbn))
                {
                    query += $" and Isbn like @isbn";
                }

                if (request.Price != null && request.Price > 0)
                {
                    query += $" and CoverPrice >= @price";
                }

                if (request.Status != null )
                {
                    query += $" and status = @status";
                }
                return query;
            }

            private static object GetParamValue(GetBooksQuery request)
            {
                return new
                {
                    name = $"%{request.Name}%",
                    isbn = $"%{request.Isbn}%",
                    price = request.Price,
                    status = request.Status != null ? ((bool)request.Status ? 1 : 0) : 0
                };
               
            }

        }
    }
}
