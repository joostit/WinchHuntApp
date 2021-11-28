using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db;

namespace WinchHuntApp.Server.Services
{
    public interface ISiteService
    {

        public Task<DbSite> GetSiteByHunterToken(string hunterAccessToken);

    }
}
