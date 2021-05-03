using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Services;


namespace WinchHuntApp.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UplinkController : ControllerBase
    {

        private const string uplinkAccessTokenName = "uplink-access-token";

        private readonly ILogger<UplinkController> logger;
        private IUplinkService uplinkService;

        public UplinkController(ILogger<UplinkController> logger, IUplinkService uplinkService)
        {
            this.logger = logger;
            this.uplinkService = uplinkService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UplinkPost postBody)
        {
            string accessToken = null;

            if (postBody == null)
            {
                logger.LogWarning("Empty uplink body received");
                return BadRequest("Uplink post body is required");
            }

            if (postBody.Devices == null)
            {
                logger.LogWarning("Uplink post without devices reveiced");
                return BadRequest("The Devices properties should not be null");
            }


            if (Request.Headers.ContainsKey(uplinkAccessTokenName))
            {
                accessToken = Request.Headers[uplinkAccessTokenName];
                if (String.IsNullOrWhiteSpace(accessToken))
                {
                    return Unauthorized("No access token header provided");
                }
            }
            else
            {
                return Unauthorized("No access token header provided");
            }

            try
            {
                await uplinkService.ProcessUplinkPost(accessToken, postBody);
            }
            catch(AggregateException ex)
            {
                return new BadRequestObjectResult($"Could not process uplink post: {ex.Flatten()}");
            }


            logger.LogInformation($"Processes uplink post from {Request.HttpContext.Connection.RemoteIpAddress}. Foxes: {postBody.Devices.Count}");

            return Ok();

        }
    }
}
