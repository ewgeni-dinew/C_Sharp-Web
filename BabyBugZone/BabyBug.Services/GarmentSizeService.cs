using BabyBug.Common.ViewModels.GarmentSize;
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
    public class GarmentSizeService : IGarmentSizeService
    {
        public BabyBugDbContext DbContext { get; set; }

        public GarmentSizeService(BabyBugDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public ICollection<BaseGarmentSizeModel> GetAllGarmentSizes()
        {
            var garmentsSizes = this.DbContext
                .GarmentSizes
                .ToHashSet();

            var collection = new HashSet<BaseGarmentSizeModel>();

            foreach (var size in garmentsSizes)
            {
                var model = new BaseGarmentSizeModel
                {
                    Id = size.Id,
                    Name = size.Value
                };

                collection.Add(model);
            }

            return collection;
        }

        public async Task CreateSizeAsync(CreateGarmentSizeModel model)
        {
            var values = model.Values
                .Split(new char[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var value in values)
            {
                var size = new GarmentSize
                {
                    Value = value
                };

                await this.DbContext.GarmentSizes.AddAsync(size);

                await this.DbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteSizeAsync(int id)
        {
            var size = await this.DbContext
                .GarmentSizes
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.DbContext.GarmentSizes.Remove(size);

            await this.DbContext.SaveChangesAsync();
        }
    }
}
