using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Models.Db
{
    public class DbFox
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DeviceId { get; set; }

        public DateTime LastSeen { get; set; }

        public string SiteId { get; set; }

        public DbSite Site { get; set; }

        public double LastLatitude { get; set; }

        public double LastLongitude { get; set; }

        public double LastAltitude { get; set; }

        public double LastVelocity { get; set; }

        public DateTime LastGpsTimestamp { get; set; }


    }
}
