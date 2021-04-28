using Microsoft.AspNetCore.Mvc;
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

        public UplinkController(IFoxService foxService,
            IHunterService hunterService,
            IAccessService accessService)
        {
            this.foxService = foxService;
            this.hunterService = hunterService;
            this.accessService = accessService;
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UplinkPost postBody)
        {
            Console.WriteLine("In: public async Task<IActionResult> Post([FromBody] UplinkPost postBody)");
            if (postBody == null) return BadRequest();

            if (postBody.AccessToken == null)
            {
                return Unauthorized();
            }

            if (postBody.Devices == null)
            {
                return BadRequest();
            }

            if (!accessService.IsUplinkAllowed(postBody.AccessToken))
            {
                return StatusCode(403, "Invalid API Access Token");
            }

            if (postBody.Hunter != null)
            {
                await hunterService.SetHunter(postBody.Hunter);
            }

            await foxService.ProcessFoxUpdateAsync(postBody);

            return Ok();

        }
    }
}
