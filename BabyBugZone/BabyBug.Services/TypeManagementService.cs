using BabyBug.Common.ViewModels.TypeManagement;
using BabyBug.Data.Models;
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
    public class TypeManagementService : BaseDbService, ITypeManagementService
    {
        public TypeManagementService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public AllTypesModel GetAllTypes()
        {
            var productTypes = this.DbContext
                .ProductTypes
                .Select(x => new BaseTypeModel
                {
                    Id = x.Id,
                    Type = x.Type
                })
                .ToHashSet();

            var deliveryTypes = this.DbContext
                .DeliveryTypes
                .Select(x => new BaseTypeModel
                {
                    Id = x.Id,
                    Type = x.Type
                })
                .ToHashSet();

            var paymentTypes = this.DbContext
                .PaymentTypes
                .Select(x => new BaseTypeModel
                {
                    Id = x.Id,
                    Type = x.Type
                })
                .ToHashSet();

            var model = new AllTypesModel()
            {
                PaymentTypes = paymentTypes,
                DeliveryTypes = deliveryTypes,
                ProductTypes = productTypes,
                ProductTypeName = nameof(ProductType),
                PaymentTypeName = nameof(PaymentType),
                DeliveryTypeName = nameof(DeliveryType)
            };

            return model;
        }

        public CreateTypeModel GetCreateTypeModel()
        {
            var model = new CreateTypeModel
            {
                Types = new HashSet<string>()
            };

            model.Types.Add(nameof(ProductType));

            model.Types.Add(nameof(DeliveryType));

            model.Types.Add(nameof(PaymentType));

            return model;
        }

        public async Task CreateTypeAsync(CreateTypeModel model)
        {
            if (model.Type.Equals(nameof(ProductType)))
            {
                var type = new ProductType()
                {
                    Type = model.Name
                };

                await this.DbContext.ProductTypes.AddAsync(type);
            }
            else if (model.Type.Equals(nameof(DeliveryType)))
            {
                var type = new DeliveryType()
                {
                    Type = model.Name
                };

                await this.DbContext.DeliveryTypes.AddAsync(type);
            }
            else if (model.Type.Equals(nameof(PaymentType)))
            {
                var type = new PaymentType()
                {
                    Type = model.Name
                };

                await this.DbContext.PaymentTypes.AddAsync(type);
            }

            await this.DbContext.SaveChangesAsync();
        }

        public EditTypeModel GetEditTypeModel(string typeName)
        {
            ICollection<string> types;

            switch (typeName)
            {
                case nameof(ProductType):
                    types = this.DbContext
                        .ProductTypes
                        .Select(x => x.Type)
                        .ToHashSet();
                    break;
                case nameof(PaymentType):
                    types = this.DbContext
                        .PaymentTypes
                        .Select(x => x.Type)
                        .ToHashSet();
                    break;
                case nameof(DeliveryType):
                    types = this.DbContext
                        .DeliveryTypes
                        .Select(x => x.Type)
                        .ToHashSet();
                    break;
                default:
                    types = new HashSet<string>();
                    break;
            }

            var model = new EditTypeModel
            {
                Types = types,
                ClassName = typeName
            };

            return model;
        }

        public async Task EditTypeAsync(EditTypeModel model)
        {
            switch (model.ClassName)
            {
                case nameof(ProductType):
                    var productType = await this.DbContext
                        .ProductTypes
                        .FirstOrDefaultAsync(x => x.Type.Equals(model.OldName));

                    if (!model.NewName.Equals(productType.Type))
                    {
                        productType.Type = model.NewName;
                    }
                    break;

                case nameof(PaymentType):
                    var paymentType = await this.DbContext
                         .PaymentTypes
                         .FirstOrDefaultAsync(x => x.Type.Equals(model.OldName));

                    if (!model.NewName.Equals(paymentType.Type))
                    {
                        paymentType.Type = model.NewName;
                    }
                    break;

                case nameof(DeliveryType):
                    var deliveryType = await this.DbContext
                        .DeliveryTypes
                        .FirstOrDefaultAsync(x => x.Type.Equals(model.OldName));

                    if (!model.NewName.Equals(deliveryType.Type))
                    {
                        deliveryType.Type = model.NewName;
                    }
                    break;

                default:
                    throw new ArgumentException();
            }

            await this.DbContext.SaveChangesAsync();
        }
    }
}
