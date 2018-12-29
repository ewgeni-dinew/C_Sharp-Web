using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBug.Data.Models;
using BabyBug.Data.Models.Enums;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class OrdersService : BaseCloudinaryService, IOrdersService
    {
        public OrdersService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task OrderGarmentAsync(int id, string userName, GarmentDetailsModel model)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                    && x.Status.Equals(OrderStatus.Created)
                                    && x.OrderGarments.Any(g => g.GarmentId.Equals(id)
                                                        && g.Size.Equals(model.Size)));

            if (order == null)
            {
                order = await this.DbContext
                    .Orders
                    .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                        && x.Status.Equals(OrderStatus.Created));

                if (order == null)
                {
                    order = new Order
                    {
                        UserId = user.Id,
                    };

                    await this.DbContext
                        .Orders
                        .AddAsync(order);

                    await this.DbContext
                    .SaveChangesAsync();
                }

                var orderGarment = new OrderGarments
                {
                    GarmentId = id,
                    Price = garment.Price,
                    Size = model.Size,
                    Quantity = model.Quantity,
                };
                
                order.OrderGarments
                    .Add(orderGarment);
            }
            else
            {
                var temp = await this.DbContext
                    .OrderGarments
                    .FirstOrDefaultAsync(x => x.GarmentId.Equals(id) 
                                        && x.Size.Equals(model.Size)
                                        && x.OrderId.Equals(order.Id));

                temp.Quantity += model.Quantity;
            }

            await this.DbContext
                    .SaveChangesAsync();
        }

        public async Task<ICollection<OrderViewModel>> GetAwaitingOrdersAsync()
        {
            return await GetOrdersByStatusAsync(OrderStatus.Awaiting);
        }

        public async Task<ICollection<OrderViewModel>> GetApprovedOrdersAsync()
        {
            return await GetOrdersByStatusAsync(OrderStatus.Approved);
        }

        private async Task<ICollection<OrderViewModel>> GetOrdersByStatusAsync(OrderStatus orderStatus)
        {
            var orders = this.DbContext
                            .Orders
                            .Where(x => x.Status.Equals(orderStatus))
                            .ToList();

            var model = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var user = await this.DbContext
                    .Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(order.UserId));

                var baseModel = new OrderViewModel()
                {
                    UserFullName = user.FirstName + " " + user.LastName,
                    OrderId = order.Id,
                    MadeOn = order.MadeOn_Date.ToString("dd/MM/yyyy HH:mm"),
                };

                model.Add(baseModel);
            }

            return model;
        }

        public async Task<ICollection<BaseOrderedProductModel>> GetOrderedProductsUserAsync(string username)
        {
            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id) && x.Status.Equals(OrderStatus.Created));

            var orderedProducts = this.DbContext
                .OrderGarments
                .Where(x => x.OrderId.Equals(order.Id))
                .ToList();

            var model = new HashSet<BaseOrderedProductModel>();

            foreach (var orderProduct in orderedProducts)
            {
                var garmentName = this.DbContext
                    .Garments
                    .FirstOrDefault(x => x.Id.Equals(orderProduct.GarmentId))
                    .Name;

                var temp = new BaseOrderedProductModel
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.GarmentId,
                    Price = orderProduct.Price,
                    Size = orderProduct.Size,
                    Quantity = orderProduct.Quantity,
                    Name = garmentName
                };

                model.Add(temp);
            }

            return model;
        }

        public async Task<AllOrderedProductsModel> GetAwaitingOrderedProductsAdminAsync(int id)
        {
            return await GetOrderedProductsAdminByOrderStatusAsync(id, OrderStatus.Awaiting);
        }

        public async Task<AllOrderedProductsModel> GetApprovedOrderedProductsAdminAsync(int id)
        {
            return await GetOrderedProductsAdminByOrderStatusAsync(id, OrderStatus.Approved);
        }

        private async Task<AllOrderedProductsModel> GetOrderedProductsAdminByOrderStatusAsync(int id, OrderStatus orderStatus)
        {
            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Status.Equals(orderStatus));

            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id.Equals(order.UserId));

            var orderedProducts = this.DbContext
                .OrderGarments
                .Where(x => x.OrderId.Equals(order.Id))
                .ToList();

            var productsCollection = new HashSet<BaseOrderedProductModel>();

            foreach (var orderProduct in orderedProducts)
            {
                var garment = await this.DbContext
                    .Garments
                    .FirstOrDefaultAsync(x => x.Id.Equals(orderProduct.GarmentId));

                var temp = new BaseOrderedProductModel
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.GarmentId,
                    Price = orderProduct.Price,
                    Size = orderProduct.Size,
                    Quantity = orderProduct.Quantity,
                    Name = garment.Name,
                };

                productsCollection.Add(temp);
            }

            var model = new AllOrderedProductsModel
            {
                OrderId = order.Id,
                UserFullName = user.FirstName + " " + user.LastName,
                OrderedProducts = productsCollection,
                PhoneNumber = user.PhoneNumber,
                Address = order.DeliveryDestination,
                DeliveryType = order.DeliveryType.ToString(),
                PaymentType = order.PaymentType.ToString(),
                City = user.City,
                DeliveryAddress = order.DeliveryDestination,
                OrderStatus = orderStatus.ToString()
            };

            return model;
        }

        public async Task RemoveProductFromOrderAsync(int orderId, int productId, string size)
        {
            var orderGarment = await this.DbContext.OrderGarments
                .FirstOrDefaultAsync(x => x.OrderId.Equals(orderId) &&
                                    x.GarmentId.Equals(productId)
                                    && x.Size.Equals(size));

            if (orderGarment != null)
            {
                this.DbContext.OrderGarments.Remove(orderGarment);
                await this.DbContext.SaveChangesAsync();
            }
        }

        public async Task<UserDataModel> GetUserDataModelAsync(string username)
        {
            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));

            var model = new UserDataModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Telephone = user.PhoneNumber,
                City = user.City,
                Address = user.Address,
                OrderId = order.Id
            };

            return model;
        }

        public async Task SetDeliveryInfoAsync(int orderId, UserDataModel model)
        {
            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(model.Username));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));

            //update user info
            if (model.FirstName != null)
            {
                user.FirstName = model.FirstName;
            }
            if (model.LastName != null)
            {
                user.LastName = model.LastName;
            }
            if (model.Telephone != null)
            {
                user.PhoneNumber = model.Telephone;
            }
            if (model.City != null)
            {
                user.City = model.City;
            }
            if (model.Address != null)
            {
                user.Address = model.Address;
            }
            await this.DbContext.SaveChangesAsync();

            //set payment type
            var paymentType = Enum.Parse<PaymentType>(model.PaymentType);

            order.PaymentType = paymentType;

            //set delivery type
            var deliveryType = Enum.Parse<DeliveryType>(model.DeliveryType);

            order.DeliveryType = deliveryType;

            if (deliveryType.Equals(DeliveryType.EcontToAddress) ||
               deliveryType.Equals(DeliveryType.SpeedyToAddress))
            {
                order.DeliveryDestination = user.Address;
            }
            else
            {
                order.DeliveryDestination = model.DeliveryDestination;
            }

            order.Status = OrderStatus.Awaiting;

            await this.DbContext.SaveChangesAsync();
        }

        public async Task ApproveOrderAsync(int id)
        {
            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            order.Status = OrderStatus.Approved;

            await this.DbContext.SaveChangesAsync();
        }

        public async Task SetOrderDate(int id)
        {
            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            order.MadeOn_Date = DateTime.UtcNow;

            await this.DbContext
                .SaveChangesAsync();
        }
    }
}
