using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Server.Services
{
    public class AccountService : IAccountService
    {

        private readonly InMemoryDbContext inMemoryDb;
        private readonly WinchHuntDbContext dbContext;

        public AccountService(WinchHuntDbContext ctx, InMemoryDbContext inMemoryDb)
        {
            dbContext = ctx;
            this.inMemoryDb = inMemoryDb;
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
