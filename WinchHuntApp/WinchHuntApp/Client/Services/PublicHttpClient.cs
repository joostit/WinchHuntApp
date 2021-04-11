using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Services
{
    public class PublicHttpClient
    {

        public HttpClient Client { get; }

        public PublicHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

    }
}
