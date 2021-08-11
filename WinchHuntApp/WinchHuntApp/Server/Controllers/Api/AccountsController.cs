using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Services;
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

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Users>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
