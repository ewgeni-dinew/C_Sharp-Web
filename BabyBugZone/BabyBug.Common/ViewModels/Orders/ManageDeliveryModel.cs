using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class ManageDeliveryModel
    {
        public string ErrorMessage { get; set; }

        public string Username { get; set; }

        public UserDataModel UserDataModel { get; set; }

        public DeliveryDataModel DeliveryDataModel { get; set; }
    }
}
