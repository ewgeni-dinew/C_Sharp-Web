using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBug.Data.Models;
using BabyBug.Data.Models.Enums;
using BabyBug.Data.Models.OrderProducts;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class OrderService : BaseDbService, IOrderService
    {
        public OrderService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task OrderProductAsync(int id, string userName, ProductDetailsModel model)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                    && x.Status.Equals(OrderStatus.Created)
                                    && x.OrderProducts.Any(g => g.ProductId.Equals(id)
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

                var orderProduct = new OrderProduct
                {
                    ProductId = id,
                    Price = product.Price,
                    Size = model.Size,
                    Quantity = model.Quantity,
                };

                order.OrderProducts
                    .Add(orderProduct);
            }
            else
            {
                var temp = await this.DbContext
                    .OrderProducts
                    .FirstOrDefaultAsync(x => x.ProductId.Equals(id)
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
            var model = new HashSet<BaseOrderedProductModel>();

            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                    && x.Status.Equals(OrderStatus.Created));

            if (order == null)
            {
                return model;
            }

            var orderedProducts = this.DbContext
                .OrderProducts
                .Where(x => x.OrderId.Equals(order.Id))
                .ToList();


            foreach (var orderProduct in orderedProducts)
            {
                var product = await this.DbContext
                    .Products
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.CategoryId
                    })
                    .FirstOrDefaultAsync(x => x.Id.Equals(orderProduct.ProductId));

                var productType = await this.GetProductTypeAsync(product.CategoryId);

                var temp = new BaseOrderedProductModel
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId,
                    Price = orderProduct.Price,
                    Size = orderProduct.Size,
                    Quantity = orderProduct.Quantity,
                    Name = product.Name,
                    ProductType = productType
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

        public async Task RemoveProductFromOrderAsync(int orderId, int productId, string size)
        {
            var orderProduct = await this.DbContext.OrderProducts
                .FirstOrDefaultAsync(x => x.OrderId.Equals(orderId) &&
                                    x.ProductId.Equals(productId)
                                    && x.Size.Equals(size));

            if (orderProduct != null)
            {
                this.DbContext
                    .OrderProducts
                    .Remove(orderProduct);

                await this.DbContext
                    .SaveChangesAsync();
            }
        }

        public async Task<ManageDeliveryModel> GetUserDataModelAsync(string username)
        {
            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            var model = new ManageDeliveryModel()
            {
                ErrorMessage = string.Empty,
                Username = user.UserName,
            };

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                    && x.Status.Equals(OrderStatus.Created));

            //validate order quantity
            var errorMsg = await this.ValidateOrderQuantity(order.Id);

            //there is an error
            if (!errorMsg.Equals(string.Empty))
            {
                model.ErrorMessage = errorMsg;
                return model;
            }

            var paymentTypes = this.DbContext
                .PaymentTypes
                .Select(x => x.Type)
                .ToHashSet();

            var deliveryTypes = this.DbContext
                .DeliveryTypes
                .Select(x => x.Type)
                .ToHashSet();

            var userModel = new UserDataModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Telephone = user.PhoneNumber,
                City = user.City,
                Address = user.Address,
                OrderId = order.Id,
            };

            var deliveryModel = new DeliveryDataModel
            {
                PaymentTypes = paymentTypes,
                DeliveryTypes = deliveryTypes,
            };

            model.DeliveryDataModel = deliveryModel;
            model.UserDataModel = userModel;

            return model;
        }

        public async Task<FinishedOrderModel> SetDeliveryInfoAsync(int orderId, ManageDeliveryModel model)
        {
            var user = await this.DbContext
                .Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(model.Username));

            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                                    && x.Status.Equals(OrderStatus.Created));


            //update user info
            if (!model.UserDataModel.FirstName.Equals(user.FirstName))
            {
                user.FirstName = model.UserDataModel.FirstName;
            }
            if (!model.UserDataModel.LastName.Equals(user.LastName))
            {
                user.LastName = model.UserDataModel.LastName;
            }
            if (!model.UserDataModel.Telephone.Equals(user.PhoneNumber))
            {
                user.PhoneNumber = model.UserDataModel.Telephone;
            }
            if (!model.UserDataModel.City.Equals(user.City))
            {
                user.City = model.UserDataModel.City;
            }
            if (!model.UserDataModel.Address.Equals(user.Address))
            {
                user.Address = model.UserDataModel.Address;
            }

            //set payment type
            var paymentType = await this.DbContext
                .PaymentTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(model.DeliveryDataModel.PaymentType));

            order.PaymentTypeId = paymentType.Id;

            //set delivery type
            var deliveryType = await this.DbContext
                .DeliveryTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(model.DeliveryDataModel.DeliveryType));

            order.DeliveryTypeId = deliveryType.Id;

            if (!deliveryType.Type.Contains("Office"))
            {
                order.DeliveryDestination = user.Address;
            }
            else
            {
                order.DeliveryDestination = model.DeliveryDataModel.DeliveryDestination;
            }

            //decrease order products quantity
            var error = await this.DecreaseOrderProductsQuantity(order.Id);

            order.MadeOn_Date = DateTime.UtcNow;

            await this.DbContext
                .SaveChangesAsync();

            var finishedModel = new FinishedOrderModel
            {
                Error = error,
                OrderId = String.Format($"XF1:+{order.Id:D6}")
            };

            return finishedModel;
        }

        public async Task ApproveOrderAsync(int id)
        {
            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            order.Status = OrderStatus.Approved;

            await this.DbContext.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(int id)
        {
            var order = await this.DbContext
                .Orders
                .FirstOrDefaultAsync(x => x.Id.Equals(id)
                                     && x.Status.Equals(OrderStatus.Awaiting));

            this.DbContext
                .Orders
                .Remove(order);

            await this.DbContext
                .SaveChangesAsync();
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
                .OrderProducts
                .Where(x => x.OrderId.Equals(order.Id))
                .ToList();

            var productsCollection = new HashSet<BaseOrderedProductModel>();

            foreach (var orderProduct in orderedProducts)
            {
                var product = await this.DbContext
                    .Products
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.CategoryId
                    })
                    .FirstOrDefaultAsync(x => x.Id.Equals(orderProduct.ProductId));

                var productType = await this.GetProductTypeAsync(product.CategoryId);

                var temp = new BaseOrderedProductModel
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId,
                    Price = orderProduct.Price,
                    Size = orderProduct.Size,
                    Quantity = orderProduct.Quantity,
                    Name = product.Name,
                    ProductType = productType,
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
                DeliveryType = await GetDeliveryTypeName((int)order.DeliveryTypeId),
                PaymentType = await GetPaymentTypeName((int)order.PaymentTypeId),
                City = user.City,
                DeliveryAddress = order.DeliveryDestination,
                OrderStatus = orderStatus.ToString()
            };

            return model;
        }

        private async Task<string> GetPaymentTypeName(int paymentTypeId)
        {
            var type = await this.DbContext
               .PaymentTypes
               .FirstOrDefaultAsync(x => x.Id.Equals(paymentTypeId));

            return type.Type;
        }

        private async Task<string> GetDeliveryTypeName(int deliveryTypeId)
        {
            var type = await this.DbContext
               .DeliveryTypes
               .FirstOrDefaultAsync(x => x.Id.Equals(deliveryTypeId));

            return type.Type;
        }

        private async Task<string> GetProductTypeAsync(int categoryId)
        {
            var category = await this.DbContext
                    .ProductCategories
                    .Select(x => new
                    {
                        x.Id,
                        x.ProductTypeId
                    })
                    .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            var productType = await this.DbContext
                .ProductTypes
                .Select(x => new
                {
                    x.Id,
                    x.Type,
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(category.ProductTypeId));

            return productType.Type;
        }

        private async Task<string> DecreaseOrderProductsQuantity(int orderId)
        {
            var sb = new StringBuilder();

            var orderProducts = this.DbContext
                .OrderProducts
                .Where(x => x.OrderId.Equals(orderId))
                .ToHashSet();

            foreach (var op in orderProducts)
            {
                var product = await this.DbContext
                    .Products
                    .FirstOrDefaultAsync(x => x.Id.Equals(op.ProductId));

                var size = await this.DbContext
                    .ProductSizes
                    .FirstOrDefaultAsync(x => x.Value.Equals(op.Size));

                var spec = await this.DbContext
                    .ProductSpecifications
                    .FirstOrDefaultAsync(x => x.ProductId.Equals(product.Id)
                                        && x.ProductSizeId.Equals(size.Id));

                if (spec.Quantity.Equals(0))
                {
                    //product is out of stock, someone has already ordered the product...

                    this.DbContext.OrderProducts.Remove(op);

                    await this.DbContext.SaveChangesAsync();

                    sb.AppendLine($">Product \"{product.Name}\" (size: {op.Size}) has been removed from your cart due to lack of stock.");
                }
                else if (spec.Quantity < op.Quantity && spec.Quantity != 0)
                {
                    //set order quantity to the max available

                    op.Quantity = spec.Quantity;
                    spec.Quantity = 0;

                    await this.DbContext.SaveChangesAsync();

                    sb.AppendLine($">Product \"{product.Name}\" (size: {op.Size}) has a new (lower) quantity amount set.");
                }
                else if (spec.Quantity.Equals(op.Quantity))
                {
                    spec.Quantity = 0;
                    await this.DbContext.SaveChangesAsync();
                }
                else
                {
                    spec.Quantity -= op.Quantity;
                    await this.DbContext.SaveChangesAsync();
                }

                //check if product has quantity left
                if (this.DbContext.ProductSpecifications
                    .Where(x => x.ProductId.Equals(product.Id))
                    .All(x => x.Quantity <= 0))
                {
                    product.IsAvailable = false;
                }
                await this.DbContext.SaveChangesAsync();
            }

            //change order status if any products left
            if (orderProducts.Any())
            {
                var order = await this.DbContext.Orders.FirstOrDefaultAsync(x => x.Id.Equals(orderId));

                order.Status = OrderStatus.Awaiting;

                await this.DbContext.SaveChangesAsync();
            }

            return sb.ToString();
        }

        private async Task<string> ValidateOrderQuantity(int orderId)
        {
            var sb = new StringBuilder();

            var orderProducts = this.DbContext
                .OrderProducts
                .Where(x => x.OrderId.Equals(orderId))
                .ToHashSet();

            foreach (var op in orderProducts)
            {
                var product = await this.DbContext
                    .Products
                    .FirstOrDefaultAsync(x => x.Id.Equals(op.ProductId));

                var size = await this.DbContext
                    .ProductSizes
                    .FirstOrDefaultAsync(x => x.Value.Equals(op.Size));

                var spec = await this.DbContext
                    .ProductSpecifications
                    .FirstOrDefaultAsync(x => x.ProductSizeId.Equals(size.Id)
                                        && x.ProductId.Equals(op.ProductId));

                if (spec.Quantity.Equals(0))
                {
                    //product is out of stock, someone has already ordered the product...

                    this.DbContext.OrderProducts.Remove(op);

                    await this.DbContext.SaveChangesAsync();

                    sb.AppendLine($">Product \"{product.Name}\" (size: {op.Size}) has been removed from your cart due to lack of stock.");
                }
                else if (spec.Quantity < op.Quantity && spec.Quantity != 0)
                {
                    //set order quantity to the max available

                    op.Quantity = spec.Quantity;

                    await this.DbContext.SaveChangesAsync();

                    sb.AppendLine($">Product \"{product.Name}\" (size: {op.Size}) has a new (lower) quantity amount set.");
                }
            }

            return sb.ToString();
        }

    }
}
