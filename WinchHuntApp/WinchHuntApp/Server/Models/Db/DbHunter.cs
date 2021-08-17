using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Models.Db
{
    public class DbHunter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AccessToken { get; set; }

        public DateTime LastSeen { get; set; }

        public string SiteId { get; set; }

        public DbSite Site { get; set; }
    }
}
