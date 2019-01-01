using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class DeleteProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public char Gender { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CreatedOn { get; set; }
    }
}
