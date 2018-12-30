using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.Contracts
{
    public interface IProductSpecification
    {
        int ProductId { get; set; }

        ProductSize ProductSize { get; set; }

        int ProductSizeId { get; set; }

        uint Quantity { get; set; }
    }
}
