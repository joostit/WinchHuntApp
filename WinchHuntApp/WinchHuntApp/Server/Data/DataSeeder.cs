using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Models.Db.Accounts;

namespace WinchHuntApp.Server.Data
{
    public class DataSeeder
    {

        private const string adminUserId = "bec81c59-b51e-4ef0-9516-6950963880f5";
        private const string adminRoleId = "3181998d-0b28-490d-9619-ca3f35d0cf83";
        private const string siteAdminRoleId = "0ecc57b3-7919-46ff-a8cd-f40df6fdccbf";
        private const string loggedInUserRoleId = "552fba04-f975-4329-ac6c-0e744e25abeb";

        private const string demoSiteId = "1c3180a1-f3f0-42e4-ac72-0310a701d537";

        private ApplicationUser adminUser = null;

        private DbSite demoSite;

        public void Seed(ModelBuilder builder)
        {
            SeedRoles(builder);
            SeedUsers(builder);
            SeedUserRoles(builder);

            SeedSites(builder);
            seedHunters(builder);
            seedFoxes(builder);
            seedSiteUsers(builder);
        }


        private void SeedSites(ModelBuilder builder)
        {
            demoSite = new DbSite()
            {
                Id = demoSiteId,
                Name = "DemoSite",
            };

            builder.Entity<DbSite>().HasData(demoSite);
        }


        private void seedHunters(ModelBuilder builder)
        {
            DbHunter hunter = new DbHunter()
            {
                AccessToken = "0011DEMO2233",
                Id = "6f608305-8590-4c45-ac37-20127f1d1e3a",
                Name = "DemoHunter",
                SiteId = demoSite.Id
            };

            builder.Entity<DbHunter>().HasData(hunter);
        }


        private void seedSiteUsers(ModelBuilder builder)
        {
            ApplicationUserDbSite userSite = new ApplicationUserDbSite()
            {
                ApplicationUserId = adminUser.Id,
                DbSiteId = demoSite.Id
            };
            builder.Entity<ApplicationUserDbSite>().HasData(userSite);
        }


        private void seedFoxes(ModelBuilder builder)
        {
            DbFox fox = new DbFox()
            {
                Id = "c3722555-1108-4186-8c47-a52f54ba2360",
                Name = "FakeFox1",
                DeviceId = "112233",
                SiteId = demoSite.Id
            };
            builder.Entity<DbFox>().HasData(fox);

            fox = new DbFox()
            {
                Id = "9618a3b6-587b-49da-b898-4a6eb62380db",
                Name = "FakeFox2",
                DeviceId = "AABBCC",
                SiteId = demoSite.Id
            };
            builder.Entity<DbFox>().HasData(fox);

            fox = new DbFox()
            {
                Id = "861d666b-0f3c-4a44-b1e5-982dc5dc8a18",
                Name = "FakeFox3",
                DeviceId = "55EE66",
                SiteId = demoSite.Id
            };
            builder.Entity<DbFox>().HasData(fox);

            fox = new DbFox()
            {
                Id = "a8fe26a8-ec1e-4b4f-8ccc-6bf122bb8092",
                Name = "FakeFox4",
                DeviceId = "77FF88",
                SiteId = demoSite.Id
            };
            builder.Entity<DbFox>().HasData(fox);

            fox = new DbFox()
            {
                Id = "1fb4aeca-dc23-4056-b26c-c6a9a19e5b4f",
                Name = "CalMarker",
                DeviceId = "112233",
                SiteId = demoSite.Id
            };
            builder.Entity<DbFox>().HasData(fox);
        }


        private void SeedUsers(ModelBuilder builder)
        {

            adminUser = new ApplicationUser()
            {
                Id = adminUserId,
                UserName = "admin@winchhunt.net",
                Email = "admin@winchhunt.net",
                NormalizedUserName = "ADMIN@WINCHHUNT.NET",
                NormalizedEmail = "ADMIN@WINCHHUNT.NET",
                LockoutEnabled = false,
                PhoneNumber = "",
                EmailConfirmed = true,
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "123Admin!");

            builder.Entity<ApplicationUser>().HasData(adminUser);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole()
                {
                    Id = siteAdminRoleId,
                    Name = "SiteManager",
                    ConcurrencyStamp = "2",
                    NormalizedName = "SITEMANAGER"
                },
                new IdentityRole()
                {
                    Id = loggedInUserRoleId,
                    Name = "LoggedInUser",
                    ConcurrencyStamp = "3",
                    NormalizedName = "LOGGEDINUSER"
                }
            );
        }


        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            );
        }


    }
}
