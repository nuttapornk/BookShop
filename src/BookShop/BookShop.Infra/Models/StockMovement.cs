using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infra.Models
{
    [Table("StockMovement")]
    public class StockMovement
    {
        public int Id { get; set; }

        [Required]
        public int MovementTypeId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public DateTime TimeInsert { get; set; }

        public virtual MovementType MovementType { get; set; }

        public virtual Book Book { get; set; }
    }
}
