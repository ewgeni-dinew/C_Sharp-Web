using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class BaseOrderedProductModel
    {
        public string Name { get; set; }

        public string Size { get; set; }
                
        public uint Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
