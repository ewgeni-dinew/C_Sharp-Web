using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Categories
{
    public class ModifyCategoryModel
    {
        public HashSet<string> CategoryNames { get; set; }

        public string CategoryName { get; set; }

        public string NewName { get; set; }

        public bool Consent { get; set; }
    }
}
