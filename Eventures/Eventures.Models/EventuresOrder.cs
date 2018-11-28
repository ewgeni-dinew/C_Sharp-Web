using System;
using System.Collections.Generic;
using System.Text;

namespace Eventures.Models
{
    public class EventuresOrder
    {
        public EventuresOrder()
        {
            this.OrderedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public EventuresUser User { get; set; }

        public string UserId { get; set; }

        public EventuresEvent Event { get; set; }

        public int EventId { get; set; }

        public int TicketsCount { get; set; }
    }
}
