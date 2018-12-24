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

                order = await this.DbContext.Orders.FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));

                if (order == null)
                {
                    order = new Order
                    {
                        UserId = user.Id,
                    };

                    await this.DbContext.Orders.AddAsync(order);

                    await this.DbContext
                    .SaveChangesAsync();
                }

                order.OrderGarments
                    .Add(orderGarment);
            }
            else
            {
                var temp = await this.DbContext
                    .OrderGarments
                    .FirstOrDefaultAsync(x => x.GarmentId.Equals(id) && x.Size.Equals(model.Size));

                temp.Quantity += model.Quantity;
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
                var user = this.DbContext
                    .Users
                    .FirstOrDefault(x => x.Id.Equals(order.UserId));

                var baseOrder = new BaseOrderModel
                {
                    UserName = user.UserName,
                    FullName = user.FirstName + " " + user.LastName
                };

                model.Add(baseOrder);
            }

            return model;
        }

        public ICollection<BaseOrderedProductModel> GetOrderedProducts(string username)
        {
            var userId = this.DbContext
                .Users
                .FirstOrDefault(x => x.UserName.Equals(username))
                .Id;

            var orderId = this.DbContext
                .Orders
                .FirstOrDefault(x => x.UserId.Equals(userId))
                .Id;

            var orderedProducts = this.DbContext
                .OrderGarments
                .Where(x => x.OrderId.Equals(orderId))
                .ToList();

            var model = new HashSet<BaseOrderedProductModel>();

            foreach (var product in orderedProducts)
            {
                var garmentName = this.DbContext
                    .Garments
                    .FirstOrDefault(x => x.Id.Equals(product.GarmentId))
                    .Name;

                var temp = new BaseOrderedProductModel
                {
                    Price = product.Price,
                    Size = product.Size,
                    Quantity = product.Quantity,
                    Name = garmentName
                };

                model.Add(temp);
            }

            return model;
        }
    }
}
