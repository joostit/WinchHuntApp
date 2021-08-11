using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WinchHuntApp.Client.Utils;
using WinchHuntApp.Shared.Identity;

namespace WinchHuntApp.Client.Services
{
    public class UserService : IUserService
    {

        private const string getUsersUrl = "api/accounts";

        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {

            GetRequest request = new GetRequest(client, getUsersUrl);

            request.Headers.Add("fox-access-token", "fakeHeader");

            return await request.Get<List<User>>();

        }
    }
}
