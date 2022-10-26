using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infra.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int BookId { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        public virtual Book Book { get; set; }
    }
}
