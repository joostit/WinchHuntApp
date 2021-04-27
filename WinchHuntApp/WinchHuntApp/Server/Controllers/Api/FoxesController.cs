using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Services;
using WinchHuntApp.Shared.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WinchHuntApp.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoxesController : ControllerBase
    {

        private IFoxService foxService;

        public FoxesController(IFoxService foxService)
        {
            this.foxService = foxService;
        }

        // GET: api/<FoxesController>
        [HttpGet]
        public async Task<IEnumerable<WinchFox>> Get()
        {
            return await foxService.GetFoxesAsync();
        }

    }
}
