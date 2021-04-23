using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Client.Services
{
    public class FoxService
    {

        private const string getFoxesUrl = "";

        private PublicHttpClient http;
        private readonly IJSRuntime jsRuntime;


        public FoxService(PublicHttpClient http, IJSRuntime js)
        {
            jsRuntime = js;
            this.http = http;
        }



        public Dictionary<string, WinchFox> GetFoxes()
        {
            return null;
        }


    }
}
