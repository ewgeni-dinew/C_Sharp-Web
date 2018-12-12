using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class GarmentSize
    {
        public GarmentSize()
        {
            this.Specifications = new HashSet<GarmentSpecification>();
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public ICollection<GarmentSpecification> Specifications { get; set; }
    }
}
