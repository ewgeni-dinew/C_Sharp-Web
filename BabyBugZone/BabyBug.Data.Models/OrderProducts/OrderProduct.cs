using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.OrderProducts
{
    public class OrderProduct : IOrderProduct
    {
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        //public ProductType ProductType { get; set; }

        //public int ProductTypeId { get; set; }

        public uint Quantity { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }
    }
}
