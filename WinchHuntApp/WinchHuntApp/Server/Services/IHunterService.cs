using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Models.Inmemory;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public interface IHunterService
    {

        Task SetHunter(MemDbSite site, WinchHunter hunter);

        Task<HunterDto> GetHunter();
    }
}
