using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WinchHuntApp.Client.Utils;
using WinchHuntApp.Shared.Dto;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Client.Services
{
    public class UserService : IUserService
    {

        private const string getUsersUrl = "api/accounts";
        private const string inviteUserUrl = "api/accounts/inviteNew";

        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            GetRequest request = new GetRequest(client, getUsersUrl);
            return await request.Get<List<User>>();
        }


        public async Task<ResultInfo> InviteNew(NewUser user)
        {
            PostRequest<NewUser> request = new PostRequest<NewUser>(client, inviteUserUrl, user);
            return await request.Post<ResultInfo>();
        }

        public Task<List<string>> GetAssignableRoles()
        {
            return new Task<List<string>>(() => new List<string>()
                {
                    "LoggedInUser",
                    "SiteManager",
                    "Admin"
                }
            );

        }
    }
}
