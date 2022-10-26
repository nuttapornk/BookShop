﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public virtual ICollection<GoodsReceiptItem> Items { get; set; }
    }
}
