using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class AllEventsViewModel<T>
    {
        public ICollection<T> Events { get; set; }
    }
}
