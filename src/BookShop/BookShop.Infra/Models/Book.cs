using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infra.Models
{
    [Table("Book")]
    public class Book
    {
        public Book()
        {
            this.BookImages = new HashSet<BookImage>();
            this.BookCategories = new HashSet<BookCategory>();  
            this.GoodsReceiptItems = new HashSet<GoodsReceiptItem>();
            this.StockMovements = new HashSet<StockMovement>();
        }
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string NameThai { get; set; }

        [StringLength(100)]
        public string NameEng { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public int? PublisherId { get; set; }

        [StringLength(1000)]
        public string Abstract { get; set; }

        [StringLength(13)]
        public string Isbn { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? CoverPrice { get; set; }        

        [Required]
        public int Status { get; set; }

        [Required, StringLength(20)]
        public string UserInsert { get; set; }

        [Required]
        public DateTime TimeInsert { get; set; }

        [Required, StringLength(20)]
        public string UserUpdate { get; set; }

        [Required]
        public DateTime TimeUpdate { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookImage> BookImages { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<GoodsReceiptItem> GoodsReceiptItems { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<StockMovement> StockMovements { get; set; }
    }
}
