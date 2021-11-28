using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db.Accounts;

namespace WinchHuntApp.Server.Models.Db
{
    public class DbSite
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<DbHunter> Hunters { get; set; }

        public ICollection<DbFox> Foxes { get; set; }

        public ICollection<ApplicationUserDbSite> Users { get; set; }
    }
}
