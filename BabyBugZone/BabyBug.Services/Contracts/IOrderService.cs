using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IOrderService : IBaseDbService
    {
        Task OrderProductAsync(int id, string userName, ProductDetailsModel model);

        Task<ICollection<OrderViewModel>> GetAwaitingOrdersAsync();

        Task<ICollection<OrderViewModel>> GetApprovedOrdersAsync();

        Task<ICollection<BaseOrderedProductModel>> GetOrderedProductsUserAsync(string username);

        Task<AllOrderedProductsModel> GetAwaitingOrderedProductsAdminAsync(int id);

        Task<AllOrderedProductsModel> GetApprovedOrderedProductsAdminAsync(int id);

        Task RemoveProductFromOrderAsync(int orderId, int productId, string size);

        Task<ManageDeliveryModel> GetUserDataModelAsync(string username);

        Task<FinishedOrderModel> SetDeliveryInfoAsync(int orderId, ManageDeliveryModel model);

        Task ApproveOrderAsync(int id);

        Task RemoveOrderAsync(int id);
    }
}
