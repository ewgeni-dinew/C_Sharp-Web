using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Services.Contracts
{
    public interface IBaseDbService
    {
        BabyBugDbContext DbContext { get; set; }
    }
}
