using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.TypeManagement
{
    public class EditTypeModel
    {
        public ICollection<string> Types { get; set; }

        public string ClassName { get; set; }

        public string OldName { get; set; }

        public string NewName { get; set; }
    }
}
