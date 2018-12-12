using BabyBug.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BabyBugZone.Data
{
    public class BabyBugDbContext : IdentityDbContext<BabyBugUser>
    {
        public BabyBugDbContext(DbContextOptions<BabyBugDbContext> options)
            : base(options)
        {
        }
    }
}
