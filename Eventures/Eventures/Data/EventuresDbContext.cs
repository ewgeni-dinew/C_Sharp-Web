using System;
using System.Collections.Generic;
using System.Text;
using Eventures.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Data
{
    public class EventuresDbContext : IdentityDbContext
    {
        public DbSet<EventuresUser> Users { get; set; }
        public DbSet<EventuresEvent> Events { get; set; }
        public DbSet<EventuresOrder> Orders { get; set; }

        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options)
        {
        }
    }
}
