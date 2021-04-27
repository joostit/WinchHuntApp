using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public interface IHunterService
    {

        Task SetHunter(WinchHunter hunter);

        Task<HunterDto> GetHunter();
    }
}
