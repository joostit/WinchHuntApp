using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Models.Db.Accounts;

namespace WinchHuntApp.Server.Data
{
    public class WinchHuntDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {


        public DbSet<DbFox> Foxes { get; set; }

        public DbSet<DbHunter> Hunters { get; set; }

        public DbSet<DbSite> Sites { get; set; }

        public DbSet<ApplicationUserDbSite> UserSites { get; set; }

        public WinchHuntDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CreateDatabase(builder);


            DataSeeder userSeeder = new DataSeeder();
            userSeeder.Seed(builder);
        }



        private void CreateDatabase(ModelBuilder builder)
        {
            builder.Entity<DbSite>(site =>
            {

                site.Property(s => s.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            });


            builder.Entity<DbHunter>(hunter =>
            {
                hunter.Property(h => h.Name)
                .IsUnicode()
                .HasMaxLength(64);

                hunter.HasOne(h => h.Site)
                .WithMany(s => s.Hunters)
                .HasForeignKey(h => h.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

                hunter.Property(h => h.AccessToken)
                .IsUnicode()
                .HasMaxLength(128);
            });


            builder.Entity<DbFox>(fox =>
            {
                fox.Property(f => f.DeviceId)
                .IsUnicode()
                .HasMaxLength(64)
                .IsRequired();

                fox.Property(f => f.Name)
                .IsUnicode()
                .HasMaxLength(64);

                fox.HasOne(f => f.Site)
                .WithMany(s => s.Foxes)
                .HasForeignKey(f => f.SiteId)
                .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<ApplicationUserDbSite>(au =>
            {
                au.HasKey(a => new {a.ApplicationUserId , a.DbSiteId });

                au.HasOne(a => a.Site)
                .WithMany(s => s.Users)
                .HasForeignKey(a => a.DbSiteId);

                au.HasOne(a => a.User)
                .WithMany(s => s.Sites)
                .HasForeignKey(a => a.ApplicationUserId);
            });

        }
    }
}
              