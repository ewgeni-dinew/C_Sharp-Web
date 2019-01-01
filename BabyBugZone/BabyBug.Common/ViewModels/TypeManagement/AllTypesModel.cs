using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.TypeManagement
{
    public class AllTypesModel
    {
        public ICollection<BaseTypeModel> ProductTypes { get; set; }

        public ICollection<BaseTypeModel> DeliveryTypes { get; set; }

        public ICollection<BaseTypeModel> PaymentTypes { get; set; }
    }
}
