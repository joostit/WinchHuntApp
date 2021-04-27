using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Services
{
    public class AccessService : IAccessService
    {

        private string apiAccessToken = "";

        public AccessService(IConfiguration configuration)
        {
            apiAccessToken = configuration["Configuration:ApiToken"];
        }


        public bool IsUplinkAllowed(string apiTokenToTest)
        {

            return String.Equals(apiAccessToken, apiTokenToTest);

        }
    }
}
