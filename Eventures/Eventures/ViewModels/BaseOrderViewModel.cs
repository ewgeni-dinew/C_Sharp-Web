using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class BaseOrderViewModel
    {
        public string Name { get; set; }

        public string Customer { get; set; }

        public DateTime OrderedOn { get; set; }
        
    }
}
