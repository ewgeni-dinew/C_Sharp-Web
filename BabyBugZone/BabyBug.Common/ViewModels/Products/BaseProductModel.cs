using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Products
{
    public class BaseProductModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public int TypeId { get; set; }
    }
}
