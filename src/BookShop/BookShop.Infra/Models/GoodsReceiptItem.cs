using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Infra.Models
{
    [Table("GoodsReceiptItem")]
    public class GoodsReceiptItem
    {
        public int GoodsReceiptId { get; set; }

        public int Num { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Qty { get; set; }

        public virtual GoodsReceipt GoodsReceipt { get; set; }

        public virtual Book Book { get; set; }
    }
}
