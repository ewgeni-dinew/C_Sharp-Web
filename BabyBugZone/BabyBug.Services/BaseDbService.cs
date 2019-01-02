using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Services
{
    public abstract class BaseDbService
    {
        protected BaseDbService(BabyBugDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public BabyBugDbContext DbContext { get; set; }
    }
}
