using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Models.Db.Accounts;
using WinchHuntApp.Shared.Dto;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Server.Services
{
    public class AccountService : IAccountService
    {

        private readonly InMemoryDbContext inMemoryDb;
        private readonly WinchHuntDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;


        public AccountService(WinchHuntDbContext ctx, 
            InMemoryDbContext inMemoryDb, 
            UserManager<ApplicationUser> userManager, 
            IEmailSender emailSender)
        {
            dbContext = ctx;
            this.inMemoryDb = inMemoryDb;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = from u in dbContext.Users
                        select new User()
                        {
                            Id = u.Id,
                            Name = u.UserName,
                            Email = u.Email
                        };

            List<User> users = await query.ToListAsync();
            foreach (var user in users)
            {
                user.Roles = await GetUserRoles(user.Id);
            }

            return users;
        }


        public async Task<ResultInfo> InviteNew(NewUser newUser, string baseUrl)
        {
            try
            {
                var user = await CreateNewInactiveUser(newUser);

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = $"{baseUrl}/Identity/Account/ResetPassword?code={code}";

                await emailSender.SendEmailAsync(
                    user.Email,
                    "WinchHunt Invite",
                    $"You have been invited to create an account at WinchHunt.Net." +
                    $"You can accept the invitation by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return ResultInfo.Success();
            }
            catch(InvalidOperationException e)
            {
                return ResultInfo.Error(e.Message);
            }
        }


        /// <summary>
        /// Creates a new inactive user with a randomized password
        /// </summary>
        /// <param name="newUser">The new user details</param>
        /// <returns>The newly created user</returns>
        private async Task<ApplicationUser> CreateNewInactiveUser(NewUser newUser)
        {

            ApplicationUser existing = await dbContext.Users.Where(u => u.NormalizedEmail.Equals(newUser.EmailAdress.ToUpper())).FirstOrDefaultAsync();

            if(existing != null)
            {
                throw new InvalidOperationException($"A user account with email address {newUser.EmailAdress} already exists");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = newUser.EmailAdress,
                UserName = newUser.EmailAdress,
                NormalizedEmail = newUser.EmailAdress.ToUpper(),
                NormalizedUserName = newUser.EmailAdress.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }


        private async Task<IEnumerable<string>> GetUserRoles(string userId)
        {
            List<string> roles = new List<string>();

            var urQuery = from ur in dbContext.UserRoles
                          where ur.UserId.Equals(userId)
                          select ur.RoleId;

            List<string> roleIds = await urQuery.ToListAsync();

            foreach (var roleId in roleIds)
            {
                var rQuery = from r in dbContext.Roles
                             where r.Id.Equals(roleId)
                             select r.Name;

                await rQuery.ForEachAsync(r => roles.Add(r));
            }

            return roles;
        }


    }
}
