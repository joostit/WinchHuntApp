using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Services;
using WinchHuntApp.Shared.Dto;
using WinchHuntApp.Shared.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WinchHuntApp.Server.Controllers
{
    //ToDo: Restrict controller to authenticated users!
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, SiteManager")]
    public class AccountsController : ControllerBase
    {

        private IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: api/<Users>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<User>> Get()
        {
            return await accountService.GetUsers();
        }


        [HttpPost("invitenew")]
        public async Task<ResultInfo> InviteNew([FromBody] NewUser newUser)
        {
            var baseUrl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, Url.Content("~"));

            return await accountService.InviteNew(newUser, baseUrl);

        }
    }
}
