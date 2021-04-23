using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Services
{
    public interface IMapService
    {
        string ApiKey { get; }
        Task Initialize();
    }
}
