using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Models.Inmemory;

namespace WinchHuntApp.Server.Services
{
    public interface IUplinkAccessService
    {
        Task<MemDbSite> GetUplinkSite(string apiToken);
    }
}
