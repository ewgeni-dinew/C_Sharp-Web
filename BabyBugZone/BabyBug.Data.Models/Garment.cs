using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Data.Models
{
    public class Garment
    {
        public Garment()
            : base()
        {
            this.Specifications = new HashSet<GarmentSpecification>();
            this.IsAvailable = true;
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public char Gender { get; set; }

        public string Description { get; set; }

        public GarmentCategory Category { get; set; }

        public int CategoryId { get; set; }

        [DataType(DataType.Currency)]
        ////[Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<GarmentSpecification> Specifications { get; set; }
    }
}
