using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db.Accounts;

namespace WinchHuntApp.Server.Data
{
    public class WinchHuntDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        
        public WinchHuntDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            UserDataSeeder userSeeder = new UserDataSeeder();
            userSeeder.Seed(builder);
        }


    }
}
 