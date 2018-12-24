using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class OrderGarments
    {
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Garment Garment { get; set; }

        public int GarmentId { get; set; }

        public uint Quantity { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }
    }
}
