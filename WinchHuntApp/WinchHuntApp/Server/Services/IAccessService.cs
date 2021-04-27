using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Services
{
    public interface IAccessService
    {
        bool IsUplinkAllowed(string apiToken);
    }
}
