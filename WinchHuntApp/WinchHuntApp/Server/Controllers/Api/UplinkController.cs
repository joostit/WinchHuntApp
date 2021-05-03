using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Services;


namespace WinchHuntApp.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UplinkController : ControllerBase
    {

        private readonly IFoxService foxService;

        private readonly IHunterService hunterService;

        private readonly IAccessService accessService;

        private readonly ILogger<UplinkController> logger;

        public UplinkController(IFoxService foxService,
            IHunterService hunterService,
            IAccessService accessService,
            ILogger<UplinkController> logger)
        {
            this.foxService = foxService;
            this.hunterService = hunterService;
            this.accessService = accessService;
            this.logger = logger;
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UplinkPost postBody)
        {
            if (postBody == null)
            {
                logger.LogWarning("Empty uplink body received");
                return BadRequest("Uplink post body is required");
            }

            if (postBody.AccessToken == null)
            {
                logger.LogWarning("null Access token received");
                return Unauthorized("No access token provided");
            }

            if (postBody.Devices == null)
            {
                logger.LogWarning("Uplink post without devices reveiced");
                return BadRequest("The Devices properties should not be null");
            }

            if (!accessService.IsUplinkAllowed(postBody.AccessToken))
            {
                logger.LogWarning($"Uplink post with invaid access token received $'{postBody.AccessToken}'");
                return StatusCode(403, "Invalid API Access Token");
            }

            //if (postBody.Hunter != null)
            //{
            //    try
            //    {
            //        await hunterService.SetHunter(postBody.Hunter);
            //    }
            //    catch (AggregateException e)
            //    {
            //        logger.LogWarning($"Exception while processing hunter uplink data $'{e.Flatten().ToString()}'");
            //        return BadRequest("Invalid Hunter data");
            //    }
            //}

            //try
            //{
            //    await foxService.ProcessFoxUpdateAsync(postBody);
            //}
            //catch (AggregateException e)
            //{
            //    logger.LogWarning($"Exception while processing foxes uplink data $'{e.Flatten().ToString()}'");
            //    return BadRequest("Error while processing foxes data");
            //}

            //logger.LogInformation($"Processes uplink post from {Request.HttpContext.Connection.RemoteIpAddress}. Foxes: {postBody.Devices.Count}");

            return Ok();

        }
    }
}
