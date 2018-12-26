using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class AllOrderedProductsModel
    {
        public ICollection<BaseOrderedProductModel> OrderedProducts { get; set; }

        public int OrderId { get; set; }

        [Display(Name = "Full Name")]
        public string UserFullName { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Delivery type")]
        public string DeliveryType { get; set; }

        [Display(Name = "Payment type")]
        public string PaymentType { get; set; }

        [Display(Name = "Delivery/Office address")]
        public string DeliveryAddress { get; set; }

    }
}
