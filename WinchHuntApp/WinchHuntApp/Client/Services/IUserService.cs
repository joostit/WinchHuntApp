using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Client.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<ResultInfo> InviteNew(NewUser user);
    }
}
