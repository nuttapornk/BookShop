using BookShop.Infra.Models;
using System.ComponentModel.DataAnnotations;

namespace BookShop.WebUi.Mediator.Books.Queries.GetBooks
{
    public class GetBooksRes
    {
        public int Id { get; set; }

        [Display(Name ="ชื่อภาษาไทย")]
        public string NameThai { get; set; }

        [Display(Name = "ชื่อภาษาอังกฤษ")]
        public string NameEng { get; set; }


        public string Isbn { get; set; }
        public decimal? CoverPrice { get; set; }
        public string Author { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
