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
            logger.LogWarning("Inside Post");


            //if (postBody == null) return BadRequest();

            //if (postBody.AccessToken == null)
            //{
            //    return Unauthorized();
            //}

            //if (postBody.Devices == null)
            //{
            //    return BadRequest();
            //}

            //if (!accessService.IsUplinkAllowed(postBody.AccessToken))
            //{
            //    return StatusCode(403, "Invalid API Access Token");
            //}

            //if (postBody.Hunter != null)
            //{
            //    await hunterService.SetHunter(postBody.Hunter);
            //}

            //await foxService.ProcessFoxUpdateAsync(postBody);

            return Ok();

        }
    }
}
