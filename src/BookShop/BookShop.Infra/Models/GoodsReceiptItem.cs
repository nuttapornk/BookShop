using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infra.Models
{
    public class GoodsReceiptItem
    {
        public int GoodsReceiptId { get; set; }//Key
        public int Num { get; set; }//Key

        public virtual GoodsReceipt GoodsReceipt { get; set; }
    }
}
