using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WinchHuntApp.Client.Utils.Http;

namespace WinchHuntApp.Client.Utils
{
    public class PostRequest<TRequestBody> : HttpRequestBase
         where TRequestBody : class
        
    {

        private TRequestBody body;

        public PostRequest(HttpClient client, string url, TRequestBody body)
            : base(client, url, HttpMethod.Post)
        {
            this.body = body;
        }

        public async Task<TResponseBody> Post<TResponseBody>()
        {
            return await base.Execute<TResponseBody, TRequestBody>(body);
        }

    }
}
