using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data.Db;
using WinchHuntApp.Server.Models;

namespace WinchHuntApp.Server.Data
{
    public class WinchHuntContext : ApiAuthorizationDbContext<ApplicationUser>
    {


        public WinchHuntContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {





        }
    }
}
