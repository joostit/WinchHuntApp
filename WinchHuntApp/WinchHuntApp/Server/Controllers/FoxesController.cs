using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WinchHuntApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoxesController : ControllerBase
    {
        // GET: api/<FoxesController>
        [HttpGet]
        public IEnumerable<WinchFox> Get()
        {
            return GetFakeFoxes();
        }


        public List<WinchFox> GetFakeFoxes()
        {
            List<WinchFox> foxes = new List<WinchFox>()
            {
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.271884,
                        Longitude = 6.882393
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeA",
                        Id = "112233"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.278816, 
                        Longitude = 6.899221,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeB",
                        Id = "AABBCC"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.278784, 
                        Longitude = 6.899211,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeC",
                        Id = "BBCCDD"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.275612, 
                        Longitude = 6.894939,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeD",
                        Id = "223344"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.270090, 
                        Longitude = 6.876612,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "Target",
                        Id = "010101"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                }
            };


            return foxes;
        }


        // GET api/<FoxesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

    }
}
