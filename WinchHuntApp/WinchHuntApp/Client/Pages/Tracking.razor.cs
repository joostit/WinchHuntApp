using GoogleMapsComponents;
using GoogleMapsComponents.Maps;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Pages
{
    public partial class Tracking : ComponentBase, IDisposable
    {

        private ElementReference mapDiv;
        private GoogleMap map;
        private MapOptions mapOptions;
        private Circle accuracyCircle;
        private Marker currentLocationMarker;

        protected override async Task OnInitializedAsync()
        {
            InitMap();
            locationService.GeoLocationStateHasChanged += LocationUpdated;
        }


        private void LocationUpdated(object s, EventArgs e)
        {
            Console.WriteLine("SiteMap: Location Updated");
            var loc = locationService.CurrentPosition;
            Console.WriteLine($"Lat: {loc.Location.Coords.Latitude}, {loc.Location.Coords.Longitude} - Accuracy: {loc.Location.Coords.Accuracy}");

            UpdateLocationMarker(loc.Location.Coords.Latitude, loc.Location.Coords.Longitude, loc.Location.Coords.Accuracy).Wait();

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }

        protected override async Task OnParametersSetAsync()
        {

        }


        private void InitMap()
        {
            mapOptions = GetMapOptions();
        }

        private async Task OnAfterMapInit()
        {
            await locationService.GetGeolocation();

            var location = locationService.CurrentPosition;

            await UpdateLocationMarker(location.Location.Coords.Latitude, location.Location.Coords.Longitude, location.Location.Coords.Accuracy);


        }


        private MapOptions GetMapOptions()
        {
            return new MapOptions()
            {
                Zoom = 12,
                Center = new LatLngLiteral()
                {
                    Lat = 52.999641,
                    Lng = 5.166507,
                },
                MapTypeId = MapTypeId.Sattellite,
                MapTypeControl = false,
                PanControl = false,
                RotateControl = false,
                ScaleControl = false,
                ZoomControl = false,
                StreetViewControl = false,
                GestureHandling = "greedy"
            };
        }


        private async Task UpdateLocationMarker(double lat, double lon, double accuracy)
        {
            if (currentLocationMarker == null)
            {
                Console.WriteLine("Creating new marker");
                await CreateLocationMarker(lat, lon);
                await CreateAccuracyCircle(lat, lon, accuracy);

                await map.InteropObject.SetCenter(new LatLngLiteral(lon, lat));
            }
            else
            {
                Console.WriteLine("Updating existing marker");
                await currentLocationMarker.SetPosition(new LatLngLiteral(lon, lat));
                await accuracyCircle.SetCenter(new LatLngLiteral(lon, lat));
                await accuracyCircle.SetRadius(accuracy);
            }
        }


        private async Task CreateLocationMarker(double lat, double lon)
        {
            MarkerOptions options = new MarkerOptions()
            {
                Position = new LatLngLiteral(lon, lat),
                Map = map.InteropObject,
                Icon = new Icon()
                {
                    Url = "/img/currentlocation.svg",
                    Anchor = new Point()
                    {
                        X = 8,
                        Y = 8
                    },
                    Size = new Size()
                    {
                        Width = 20,
                        Height = 20
                    },
                    ScaledSize = new Size()
                    {
                        Width = 16,
                        Height = 16
                    }
                }
            };

            currentLocationMarker = await Marker.CreateAsync(map.JsRuntime, options);
        }


        private async Task CreateAccuracyCircle(double lat, double lon, double accuracy)
        {
            accuracyCircle = await Circle.CreateAsync(map.JsRuntime, new CircleOptions()
            {
                Draggable = false,
                Editable = false,
                Map = map.InteropObject,
                Radius = accuracy,
                FillColor = "rgba(5, 192, 249, 0.48)",
                StrokeColor = "rgb(0, 128, 209)",
                Center = new LatLngLiteral(lon, lat),
                StrokeWeight = 1
            });
        }

        public void Dispose()
        {
            locationService.GeoLocationStateHasChanged -= LocationUpdated;
        }
    }
}
