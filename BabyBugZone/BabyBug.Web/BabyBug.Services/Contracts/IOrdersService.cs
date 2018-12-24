using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IOrdersService
    {
        BabyBugDbContext DbContext { get; set; }

        Task OrderGarment(int id, string userName, GarmentDetailsModel model);

        ICollection<BaseOrderModel> GetAllOrders();

        ICollection<BaseOrderedProductModel> GetOrderedProducts(string username);
    }
}
