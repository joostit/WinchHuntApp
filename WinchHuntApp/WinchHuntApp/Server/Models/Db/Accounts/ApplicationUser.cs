using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Models.Db.Accounts
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<ApplicationUserDbSite> Sites { get; set; }

    }
}
