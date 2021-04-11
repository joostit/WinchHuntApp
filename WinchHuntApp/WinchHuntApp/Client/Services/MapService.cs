using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WinchHuntApp.Client.Data;

namespace WinchHuntApp.Client.Services
{
    public class MapService : IMapService
    {

        public static int counter = 0;

        public MapsSettings Settings { get; private set; } = new MapsSettings();
        private PublicHttpClient http;

        public string ApiKey { get; set; }

        public MapService(PublicHttpClient http)
        {
            this.http = http;

            counter++;
            Console.WriteLine($"MapService instances {counter}");

        }

        public async Task Initialize()
        {
            ApiKey = await GetMapsApiKey();
        }    

        private async Task<string> GetMapsApiKey()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/maps/apiKey");
            request.Headers.Add("key-request-token", "!IReallyWantIt!");

            var response = await http.Client.SendAsync(request);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();

            return JsonSerializer.Deserialize<String>(responseBytes, 
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
    }
}

