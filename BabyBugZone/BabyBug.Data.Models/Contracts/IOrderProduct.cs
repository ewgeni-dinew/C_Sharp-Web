using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.Contracts
{
    public interface IOrderProduct
    {
        Order Order { get; set; }

        int OrderId { get; set; }

        int ProductId { get; set; }

        uint Quantity { get; set; }

        decimal Price { get; set; }

        string Size { get; set; }
    }
}
