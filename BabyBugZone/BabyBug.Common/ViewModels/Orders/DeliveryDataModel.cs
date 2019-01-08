using BabyBug.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class DeliveryDataModel
    {
        public HashSet<string> PaymentTypes { get; set; }

        [Required]
        public string PaymentType { get; set; }

        public HashSet<string> DeliveryTypes { get; set; }

        [Required]
        public string DeliveryType { get; set; }

        [StringLength(ModelConstants.DESTINATION_NAME_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.DESTINATION_NAME_LENGTH_MIN)]
        public string DeliveryDestination { get; set; }
    }
}
