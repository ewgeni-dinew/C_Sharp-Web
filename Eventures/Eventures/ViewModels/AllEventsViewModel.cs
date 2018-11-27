using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class AllEventsViewModel
    {
        public ICollection<BaseEventViewModel> Events { get; set; }
    }
}
