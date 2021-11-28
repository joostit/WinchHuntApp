using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Models.Inmemory
{
    public class MemDbSite
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UplinkAccessToken { get; set; }
    }
}
