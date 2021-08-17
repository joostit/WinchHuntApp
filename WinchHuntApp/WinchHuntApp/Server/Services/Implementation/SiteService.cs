using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Server.Models.Db;

namespace WinchHuntApp.Server.Services.Implementation
{
    public class SiteService : ISiteService
    {

        private readonly InMemoryDbContext inMemoryDb;
        private readonly WinchHuntDbContext dbContext;

        public SiteService(WinchHuntDbContext ctx, InMemoryDbContext inMemoryDb)
        {
            this.inMemoryDb = inMemoryDb;
            this.dbContext = ctx;
        }

        public async Task<DbSite> GetSiteByHunterToken(string hunterAccessToken)
        {
            var site = await dbContext.Hunters
                .Where(h => h.AccessToken.Equals(hunterAccessToken))
                .Include(h => h.Site)
                .Select(h => h.Site)
                .FirstOrDefaultAsync();

            return site;
        }
    }
}
