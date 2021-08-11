using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Client.Services;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Client.Components
{
    public partial class UserList : ComponentBase
    {

        private IEnumerable<User> users = new List<User>();

        [Inject]
        public IUserService UserService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            users = await UserService.GetUsers();
        }


        private string DisplayRoles(IEnumerable<string> roles)
        {
            string retVal = "";
            bool isFirst = true;

            foreach (var role in roles)
            {
                if (!isFirst)
                {
                    retVal += ", ";
                    isFirst = true;
                }

                retVal += role;
            }

            return retVal;
        }

    }
}
