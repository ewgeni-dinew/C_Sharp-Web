using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Data.Models
{
    public class GarmentCategory
    {
        public GarmentCategory()
        {
            this.Garments = new HashSet<Garment>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Garment> Garments { get; set; }
    }
}
