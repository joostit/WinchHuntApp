using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data.Db;

namespace WinchHuntApp.Server.Data
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options) { }

        public DbSet<DbFox> Foxes { get; set; }

        public List<DbHunter> Hunters { get; set; }

    }
}
