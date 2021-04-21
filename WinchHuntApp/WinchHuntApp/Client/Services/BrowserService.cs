using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WinchHuntApp.Client.Services
{

    public class BrowserService
    {
        private readonly IJSRuntime jsRuntime;
        public static event Func<Task> OnResize;

        public BrowserService(IJSRuntime js)
        {
            jsRuntime = js;
            jsRuntime.InvokeAsync<object>("browserResize.registerResizeCallback");
        }

        public async Task<Dimensions> GetDimensions()
        {
            return await jsRuntime.InvokeAsync<Dimensions>("getDimensions");
        }


        public async Task<Dimensions> GetInnerDimensions(String elementId)
        {
            return await jsRuntime.InvokeAsync<Dimensions>("getInnerDimensionsById", elementId);
        }


        [JSInvokable]
        public static async Task OnBrowserResize()
        {
            await OnResize?.Invoke();
        }

        public async Task<int> GetInnerHeight()
        {
            return await jsRuntime.InvokeAsync<int>("browserResize.getInnerHeight");
        }

        public async Task<int> GetInnerWidth()
        {
            return await jsRuntime.InvokeAsync<int>("browserResize.getInnerWidth");
        }


    }

    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
