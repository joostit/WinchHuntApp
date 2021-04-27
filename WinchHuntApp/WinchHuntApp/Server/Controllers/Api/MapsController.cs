using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WinchHuntApp.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {

        private readonly IConfiguration _config;

        public MapsController(IConfiguration config)
        {
            this._config = config;
        }


        // GET api/<MapsController>/
        [HttpGet("{id}")]
        public string Get(string id)
        {
            // ToDo: Make this less sketchy ;)

            if(id == "apiKey")
            {
                if (Request.Headers.ContainsKey("key-request-token"))
                {
                    if(Request.Headers["key-request-token"] == "!IReallyWantIt!")
                    {
                        string key = _config.GetValue<string>("GoogleMapsApiKey");
                        return JsonSerializer.Serialize(key);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;            
        }

    }
}
