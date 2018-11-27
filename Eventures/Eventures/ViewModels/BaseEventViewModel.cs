using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class BaseEventViewModel
    {
        public string Name { get; set; }

        public string Place { get; set; }
        
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal PricePerTicket { get; set; }
        
        public int TotalTickets { get; set; }
    }
}
