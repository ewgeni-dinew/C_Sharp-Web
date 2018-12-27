using BabyBug.Common.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IHomeService
    {
        Task<IndexViewModel> GetIndexModelAsync();
    }
}
