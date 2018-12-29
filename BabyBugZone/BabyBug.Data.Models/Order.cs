using BabyBug.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyBug.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderGarments = new HashSet<OrderGarments>();
            Status = OrderStatus.Created;
        }

        public int Id { get; set; }

        public DateTime MadeOn_Date { get; set; }

        public BabyBugUser User { get; set; }

        public string UserId { get; set; }        

        public OrderStatus Status { get; set; }

        public PaymentType PaymentType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string DeliveryDestination { get; set; }

        public ICollection<OrderGarments> OrderGarments { get; set; }
    }
}
