using BabyBugZone.Data;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Services
{
    public abstract class BaseService
    {
        protected BaseService(BabyBugDbContext DbContext)
        {
            this.Account = new Account(
                "dm6qsz74d",
                "468634173751356",
                "M-LBxFKAP9qqhMzNd8Sgsx5RxE8");

            this.Cloudinary = new Cloudinary(this.Account);

            this.DbContext = DbContext;
        }

        public BabyBugDbContext DbContext { get; set; }

        public Account Account { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
