using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public interface IFoxService
    {
        Task ProcessFoxUpdateAsync(DbSite site, UplinkPost post);
        Task<IEnumerable<WinchFox>> GetFoxesAsync();
        Task<WinchFox> GetFoxAsync(string id);
    }
}
