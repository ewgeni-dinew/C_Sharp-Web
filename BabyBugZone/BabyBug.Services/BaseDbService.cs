using AutoMapper;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Services
{
    public abstract class BaseDbService: IBaseDbService
    {
        protected BaseDbService(BabyBugDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        
        public BabyBugDbContext DbContext { get; set; }
    }
}
