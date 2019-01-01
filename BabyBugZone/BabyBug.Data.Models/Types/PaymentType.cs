using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class PaymentType
    {
        public PaymentType()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
