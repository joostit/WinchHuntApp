using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Client.Data;

namespace WinchHuntApp.Client.Services
{
    public interface IMapService
    {

        MapsSettings Settings { get; }
        string ApiKey { get; }
        Task Initialize();
    }
}
