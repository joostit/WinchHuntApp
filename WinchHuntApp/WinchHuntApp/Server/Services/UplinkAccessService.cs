using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db;

namespace WinchHuntApp.Server.Services
{
    public class UplinkAccessService : IUplinkAccessService
    {

        // Replace by real database implementation
        private DbSite defaultSite = new DbSite();


        public UplinkAccessService(IConfiguration configuration)
        {
            defaultSite.UplinkAccessToken = configuration["Configuration:ApiToken"];
        }


        public async Task<DbSite> GetUplinkSite(string uplinkAccessToken)
        {
            // ToDo: Actually search the database for the actual site

            if (String.Equals(uplinkAccessToken, defaultSite.UplinkAccessToken))
            {
                return defaultSite;
            }
            else
            {
                return null;
            }
        }
    }
}
