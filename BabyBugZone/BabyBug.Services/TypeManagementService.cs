using BabyBug.Common.ViewModels.TypeManagement;
using BabyBug.Data.Models;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class TypeManagementService : BaseCloudinaryService, ITypeManagementService
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
    }
}
