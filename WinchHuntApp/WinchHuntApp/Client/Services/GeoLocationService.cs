using BrowserInterop.Geolocation;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrowserInterop.Extensions;

namespace WinchHuntApp.Client.Services
{
    public class GeoLocationService : IDisposable, IAsyncDisposable
    {
        private readonly IJSRuntime jsRuntime;

        private WindowNavigatorGeolocation geolocationWrapper;
        public GeolocationResult CurrentPosition { get; private set; }
        private IAsyncDisposable geopositionWatcher;

        public event EventHandler GeoLocationStateHasChanged;

        public GeoLocationService(IJSRuntime js)
        {
            jsRuntime = js;
        }

        public async Task Initialize()
        {
            var window = await jsRuntime.Window();
            var navigator = await window.Navigator();
            geolocationWrapper = navigator.Geolocation;

            await StartWatchPosition();
        }


        public async Task GetGeolocation()
        {
            CurrentPosition = await geolocationWrapper.GetCurrentPosition(new PositionOptions()
            {
                EnableHighAccuracy = true,
                MaximumAgeTimeSpan = TimeSpan.FromMinutes(1),
                TimeoutTimeSpan = TimeSpan.FromMinutes(1)
            });
        }

        public async Task StartWatchPosition()
        {
            PositionOptions options = new PositionOptions()
            {
                EnableHighAccuracy = true,
                MaximumAge = 0,
                Timeout = 0
            };

            geopositionWatcher = await geolocationWrapper.WatchPosition(async (p) =>
            {
                await Task.Run(() =>
                {
                    CurrentPosition = p;
                    GeoLocationStateHasChanged?.Invoke(this, EventArgs.Empty);
                });
            },
            options);
        }

        public void Dispose()
        {
            GeoLocationStateHasChanged = null;
            if (geopositionWatcher != null)
            {
                geopositionWatcher.DisposeAsync();
                geopositionWatcher = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            GeoLocationStateHasChanged = null;
            if (geopositionWatcher != null)
            {
                await geopositionWatcher.DisposeAsync();
                geopositionWatcher = null;
            }
        }
    }
}
