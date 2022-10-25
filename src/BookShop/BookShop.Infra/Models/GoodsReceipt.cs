using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infra.Models
{
    [Table("GoodsReceipt")]
    public class GoodsReceipt
    {
        public GoodsReceipt()
        {
            this.Items = new HashSet<GoodsReceiptItem>();
        }

        public int Id { get; set; }

        public DateTime InsetDate { get; set; }

        public virtual ICollection<GoodsReceiptItem> Items { get; set; }
    }
}
