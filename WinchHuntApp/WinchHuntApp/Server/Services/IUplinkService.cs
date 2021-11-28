using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public interface IUplinkService
    {
        Task ProcessUplinkPost(string uplinkAccessToken, UplinkPost uplinkPost);
    }
}
