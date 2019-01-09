using BabyBug.Common.ViewModels.TypeManagement;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface ITypeManagementService : IBaseDbService
    {
        AllTypesModel GetAllTypes();

        CreateTypeModel GetCreateTypeModel();

        Task CreateTypeAsync(CreateTypeModel model);

        EditTypeModel GetEditTypeModel(string typeName);

        Task EditTypeAsync(EditTypeModel model);
    }
}
