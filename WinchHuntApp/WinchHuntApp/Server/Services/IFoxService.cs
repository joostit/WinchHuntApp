using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public interface IFoxService
    {
        Task ProcessFoxUpdateAsync(UplinkPost post);
        Task<IEnumerable<WinchFox>> GetFoxesAsync();
        Task<WinchFox> GetFoxAsync(string id);
    }
}
