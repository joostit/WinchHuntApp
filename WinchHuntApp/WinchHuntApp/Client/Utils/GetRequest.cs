using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WinchHuntApp.Client.Utils.Http;

namespace WinchHuntApp.Client.Utils
{
    public class GetRequest : HttpRequestBase
    {

        public GetRequest(HttpClient client, string url) 
            : base(client, url, HttpMethod.Get)
        {
        }

        public async Task<TResponseBody> Get<TResponseBody>()
        {
            return await base.Execute<TResponseBody>();
        }


    }

}
