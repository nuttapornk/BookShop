using BookShop.Infra.Models;

namespace BookShop.WebUi.Mediator.Books.Queries.GetBooks
{
    public class GetBooksRes
    {
        public int Id { get; set; }
        public string NameThai { get; set; }
        public string NameEng { get; set; }
        public string Isbn { get; set; }
        public decimal? CoverPrice { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
