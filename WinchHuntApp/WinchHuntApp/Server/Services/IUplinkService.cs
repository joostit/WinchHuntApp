using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Models;

namespace WinchHuntApp.Server.Services
{
    public interface IUplinkService
    {
        Task ProcessUplinkPost(string uplinkAccessToken, UplinkPost uplinkPost);
    }
}
