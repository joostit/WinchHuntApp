using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Utils
{
    public class GetRequest
    {

        private HttpClient client;
        private string url;

        public HeaderList Headers { get; set; } = new HeaderList();

        public GetRequest(HttpClient client, string url)
        {
            this.client = client;
            this.url = url;
        }


        public async Task<TResponseBody> Get<TResponseBody>()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            foreach(var header in Headers)
            {
                request.Headers.Add(header.Name, header.Value);
            }

            var response = await client.SendAsync(request);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();

            TResponseBody result = JsonSerializer.Deserialize<TResponseBody>(responseBytes,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return result;
        }


    }


    public class HeaderList : List<Header>
    {
        public HeaderList()
        {

        }

        public HeaderList(IEnumerable<Header> headers) 
            : base (headers)
        {

        }

        public void Add(string name, string value)
        {
            base.Add(new Header(name, value));
        }
    }


    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Header()
        {

        }

        public Header(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
