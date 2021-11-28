using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Models.Inmemory;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services.Implementation
{
    public class UplinkService : IUplinkService
    {

        ILogger<UplinkService> logger;
        IFoxService foxService;
        IHunterService hunterService;
        ISiteService siteService;

        public UplinkService(ILogger<UplinkService> logger,
                             IFoxService foxService,
                             IHunterService hunterService,
                             ISiteService siteService)
        {
            this.logger = logger;
            this.foxService = foxService;
            this.hunterService = hunterService;
            this.siteService = siteService;
        }



        public async Task ProcessUplinkPost(string uplinkAccessToken, UplinkPost postBody)
        {

            DbSite targetSite = await siteService.GetSiteByHunterToken(uplinkAccessToken);

            if (targetSite == null)
            {
                throw new AccessViolationException("Invalid or missing Uplink Access Token");
            }

            if (postBody.Hunter == null)
            {
                throw new InvalidOperationException("Hunter data not set");
            }

            if(postBody.Devices == null)
            {
                throw new InvalidOperationException("Devices collection not set");
            }


            try
            {
                await foxService.ProcessFoxUpdateAsync(targetSite, postBody);
            }
            catch (AggregateException e)
            {
                logger.LogWarning($"Exception while processing foxes uplink data $'{e.Flatten()}'");
            }

            try
            {
                await hunterService.SetHunter(targetSite, postBody.Hunter);
            }
            catch (AggregateException e)
            {
                logger.LogWarning($"Exception while processing hunter uplink data $'{e.Flatten()}'");
            }

        }
    }
}
