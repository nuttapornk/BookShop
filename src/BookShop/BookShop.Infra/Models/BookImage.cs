using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infra.Models
{
    [Table("BookImage")]
    public class BookImage
    {
        public int Id { get; set; }

        [Required,StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsCover { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

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
    }
}
