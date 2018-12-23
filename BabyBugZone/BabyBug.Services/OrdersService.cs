using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBug.Data.Models;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task OrderGarment(int id, string userName, GarmentDetailsModel model)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            var order = await this.DbContext.Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                    && x.OrderGarments.Any(g => g.GarmentId.Equals(id)
                                                        && g.Size.Equals(model.Size)));

            if (order == null)
            {
                var orderGarment = new OrderGarments
                {
                    GarmentId = id,
                    Price = garment.Price,
                    Size = model.Size,
                    Quantity = model.Quantity,
                };

                order = new Order
                {
                    UserId = user.Id,
                };

                await this.DbContext.Orders.AddAsync(order);

                await this.DbContext
                    .SaveChangesAsync();

                order.OrderGarments
                    .Add(orderGarment);
            }
            else
            {
                order.OrderGarments
                    .FirstOrDefault(x => x.GarmentId.Equals(id) && x.Size.Equals(model.Size))
                    .Quantity += model.Quantity;
            }
            await this.DbContext
                    .SaveChangesAsync();
        }

        public ICollection<BaseOrderModel> GetAllOrders()
        {
            var orders = this.DbContext
                .Orders
                .ToList();

            var model = new List<BaseOrderModel>();

            foreach (var order in orders)
            {
                var userName = this.DbContext
                    .Users
                    .FirstOrDefault(x => x.Id.Equals(order.UserId))
                    .UserName;

                var baseOrder = new BaseOrderModel
                {
                    UserName = userName,
                    Id = order.Id,
                };

                model.Add(baseOrder);
            }

            return model;
        }
    }
}
