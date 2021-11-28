using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Utils.Http
{
    public abstract class HttpRequestBase
    {

        private HttpClient client;
        private string url;

        public HeaderList Headers { get; private set; } = new HeaderList();

        private HttpMethod method;

        public HttpRequestBase(HttpClient client, string url, HttpMethod method)
        {
            this.client = client;
            this.url = url;
            this.method = method;
        }

        protected async Task<TResponseBody> Execute<TResponseBody>()
        {
            return await Execute<TResponseBody, object>(null);
        }


        protected async Task<TResponseBody> Execute<TResponseBody, TRequestBody>(TRequestBody body)
            where TRequestBody : class
        {
            var request = new HttpRequestMessage(method, url);

            // Add request bodu if applicable
            if(body != null)
            { 
                string bodyJson = JsonSerializer.Serialize(body);
                request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            }

            // Add headers
            foreach (var header in Headers)
            {
                request.Headers.Add(header.Name, header.Value);
            }

            // Send request and read response
            var response = await client.SendAsync(request);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();

            // Process the response
            string responseString = ASCIIEncoding.UTF8.GetString(responseBytes);
            TResponseBody result = JsonSerializer.Deserialize<TResponseBody>(responseBytes,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return result;
        }
    }
}
