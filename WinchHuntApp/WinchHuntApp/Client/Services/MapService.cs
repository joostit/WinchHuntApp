using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Services
{
    public class MapService : IMapService
    {

        private PublicHttpClient http;
        private readonly IJSRuntime jsRuntime;

        public string ApiKey { get; set; }

        public MapService(PublicHttpClient http, IJSRuntime js)
        {
            jsRuntime = js;
            this.http = http;

        }

        public async Task Initialize()
        {
            ApiKey = await GetMapsApiKey();

            await InitMapsJs();
        }

        private async Task InitMapsJs()
        {
            await jsRuntime.InvokeVoidAsync("initMapsScript", ApiKey);
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

