using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Server.Services
{
    public interface IAccountService
    {

        Task<IEnumerable<User>> GetUsers();

    }
}
