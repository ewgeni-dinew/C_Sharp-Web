﻿using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Products;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IProductService: IBaseCloudinaryService
    {
        CreateProductModel GetProductCreateModel();

        Task<DeleteProductModel> GetDeleteModelAsync(int id);

        Task CreateProductAsync(CreateProductModel model);

        Task DeleteProductAsync(int id);

        Task<ProductDetailsModel> GetDetailsModelAsync(int id);

        Task<EditProductModel> GetEditModelAsync(int id);

        Task EditProductAsync(int id, EditProductModel model);

        Task<ICollection<BaseProductModel>> GetOutOfStockProductsModelAsync();
    }
}
