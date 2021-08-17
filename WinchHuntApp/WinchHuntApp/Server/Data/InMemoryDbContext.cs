using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Inmemory;

namespace WinchHuntApp.Server.Data
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options) { }

        public DbSet<MemDbFox> Foxes { get; set; }

        public DbSet<MemDbHunter> Hunters { get; set; }

    }
}
