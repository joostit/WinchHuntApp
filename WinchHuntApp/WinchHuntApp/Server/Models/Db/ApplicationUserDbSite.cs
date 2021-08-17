using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db.Accounts;

namespace WinchHuntApp.Server.Models.Db
{
    public class ApplicationUserDbSite
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public string DbSiteId { get; set; }

        public DbSite Site { get; set; }
    }
}
