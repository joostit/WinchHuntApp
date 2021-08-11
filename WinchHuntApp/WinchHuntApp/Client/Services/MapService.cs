using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WinchHuntApp.Client.Utils;
using WinchHuntApp.Shared.Constants;

namespace WinchHuntApp.Client.Services
{
    public class MapService : IMapService
    {

        private const string getMapsKeyUrl = "/api/maps/apiKey";

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
            GetRequest request = new GetRequest(http.Client, getMapsKeyUrl);

            request.Headers.Add(GoogleMaps.KeyRequestHeaderName, GoogleMaps.KeyRequestHeaderValue);

            return await request.Get<string>();
        }

    }
}

