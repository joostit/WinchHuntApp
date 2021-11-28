using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Client.Services;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Client.Components
{
    public partial class NewUserForm : ComponentBase
    {

        [Inject]
        public IUserService UserService { get; set; }

        private NewUser model = new NewUser();
        private bool loading;

        private async void OnValidSubmit()
        {
            loading = true;
            try
            {
                await UserService.InviteNew(model);
                NavigationManager.NavigateTo("users");
            }
            catch
            {
                loading = false;
                StateHasChanged();
            }
        }
    }
}
