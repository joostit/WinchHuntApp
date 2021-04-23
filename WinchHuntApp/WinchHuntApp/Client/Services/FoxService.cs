using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Client.Utils;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Client.Services
{
    public class FoxService
    {

        private const string getFoxesUrl = "api/foxes";

        private PublicHttpClient publicHttp;
        private readonly IJSRuntime jsRuntime;


        public FoxService(PublicHttpClient publicHttp, IJSRuntime js)
        {
            jsRuntime = js;
            this.publicHttp = publicHttp;
        }



        public async Task<List<WinchFox>> GetFoxes()
        {
            GetRequest request = new GetRequest(publicHttp.Client, getFoxesUrl);

            request.Headers.Add("fox-access-token", "fakeHeader");

            return await request.Get<List<WinchFox>>();
        }


    }
}
