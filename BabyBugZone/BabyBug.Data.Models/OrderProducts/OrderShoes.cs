using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.OrderProducts
{
    public class OrderShoes : IOrderProduct
    {
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Shoe Product { get; set; }

        public int ProductId { get; set; }

        public uint Quantity { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }
    }
}
