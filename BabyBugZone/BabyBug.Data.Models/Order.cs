using BabyBug.Data.Models.Enums;
using BabyBug.Data.Models.OrderProducts;
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
            this.OrderProducts = new HashSet<OrderProduct>();            

            Status = OrderStatus.Created;
        }

        public int Id { get; set; }

        public DateTime MadeOn_Date { get; set; }

        public BabyBugUser User { get; set; }

        public string UserId { get; set; }        

        public OrderStatus Status { get; set; }

        public PaymentType PaymentType { get; set; }

        public int? PaymentTypeId { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public int? DeliveryTypeId { get; set; }

        public string DeliveryDestination { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
