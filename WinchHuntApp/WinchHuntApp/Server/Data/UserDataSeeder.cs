using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Models.Db.Accounts;

namespace WinchHuntApp.Server.Data
{
    public class UserDataSeeder
    {

        private const string adminUserId = "bec81c59-b51e-4ef0-9516-6950963880f5";
        private const string adminRoleId = "3181998d-0b28-490d-9619-ca3f35d0cf83";
        private const string siteAdminRoleId = "0ecc57b3-7919-46ff-a8cd-f40df6fdccbf";
        private const string loggedInUserRoleId = "552fba04-f975-4329-ac6c-0e744e25abeb";
               

        public void Seed(ModelBuilder builder)
        {
            SeedRoles(builder);
            SeedUsers(builder);
            SeedUserRoles(builder);
        }


        private void SeedUsers(ModelBuilder builder)
        {

            ApplicationUser user = new ApplicationUser()
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
            user.PasswordHash = passwordHasher.HashPassword(user, "123Admin!");

            builder.Entity<ApplicationUser>().HasData(user);
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
