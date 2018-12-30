using BabyBug.Data.Models.Categories;
using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.OrderProducts;
using BabyBug.Data.Models.ProductSpecifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BabyBug.Data.Models.Products
{
    public class Accessory: IProduct
    {
        public Accessory()
        {
            this.Specifications = new HashSet<AccessorySpecification>();

            this.OrderAccessories = new HashSet<OrderAccessories>();

            this.IsAvailable = true;

            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public char Gender { get; set; }

        public string Description { get; set; }

        public AccessoryCategory Category { get; set; }

        public int CategoryId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public ICollection<AccessorySpecification> Specifications { get; set; }

        public ICollection<OrderAccessories> OrderAccessories { get; set; }
    }
}
