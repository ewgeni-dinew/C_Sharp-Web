using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Products;

namespace BabyBug.Data.Models.ProductSpecifications
{
    public class GarmentSpecification: IProductSpecification
    {
        public Garment Product { get; set; }

        public int ProductId { get; set; }

        public ProductSize ProductSize { get; set; }

        public int ProductSizeId { get; set; }

        public uint Quantity { get; set; }
    }
}