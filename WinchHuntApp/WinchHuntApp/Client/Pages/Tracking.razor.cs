using GoogleMapsComponents;
using GoogleMapsComponents.Maps;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Client.Pages
{
    public partial class Tracking : ComponentBase, IDisposable
    {

        [Parameter]
        public string SiteId { get; set; }

        private const string targetSelected = "/img/target-green.svg";
        private const string targetFresh = "/img/target-yellow.svg";
        private const string targetOld = "/img/target-red.svg";

        private ElementReference mapDiv;
        private GoogleMap map;
        private MapOptions mapOptions;
        private Circle accuracyCircle;
        private Marker currentLocationMarker;
        private System.Threading.Timer foxUpdateTimer;
        private List<FoxMarker> foxMarkers = new List<FoxMarker>();
        private volatile bool isUpdatingFoxes = false;
        bool isUpdatingLocation = false;


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
            await StartFoxListener();
        }


        protected override async Task OnParametersSetAsync()
        {

        }


        private async Task StartFoxListener()
        {
            await Task.Run(() =>
            {
                foxUpdateTimer = new System.Threading.Timer(async (object na) =>
                {
                    if (!isUpdatingFoxes)
                    {
                        try
                        {
                            isUpdatingFoxes = true;
                            List<WinchFox> update = await foxService.GetFoxes();
                            await UpdateFoxes(update);
                        }
                        finally
                        {
                            isUpdatingFoxes = false;
                        }
                    }
                }, null, 2000, 2000);
            });
        }


        private async Task UpdateFoxes(List<WinchFox> update)
        {
            // Find foxMarkers that don't exist in the new update
            List<FoxMarker> toDelete = new List<FoxMarker>();
            foreach(var existing in foxMarkers)
            {
                var found = update.Where(fox => fox.Device.Id == existing.Fox.Device.Id).FirstOrDefault();
                if(found == null)
                {
                    toDelete.Add(existing);
                }
            }

            // Remove non-existing fox markers
            foreach(var markerToDelete in toDelete)
            {
                //await markerToDelete.Marker.SetMap(null);
                markerToDelete.Marker.Dispose();
                foxMarkers.Remove(markerToDelete);
            }
            
            // Create new- or update existing markers
            foreach(WinchFox updateFox in update)
            {
                var existing = foxMarkers.Where(f => f.Fox.Device.Id == updateFox.Device.Id).FirstOrDefault();
                if (existing == null)
                {
                    FoxMarker newMarker = await CreateFoxMarker(updateFox);
                    foxMarkers.Add(newMarker);
                }
                else
                {
                    existing.Fox = updateFox;
                    await existing.Marker.SetPosition(new LatLngLiteral(updateFox.Gps.Longitude, updateFox.Gps.Latitude));
                    await existing.Marker.SetIcon(CreateFoxIcon(updateFox.LastUpdate));
                }
            }

        }


        private async Task<FoxMarker> CreateFoxMarker(WinchFox fox)
        {
            MarkerOptions options = new MarkerOptions()
            {
                Position = new LatLngLiteral(fox.Gps.Longitude, fox.Gps.Latitude),
                Map = map.InteropObject,
                Icon = CreateFoxIcon(fox.LastUpdate)
            };

            FoxMarker marker = new();
            marker.Marker = await Marker.CreateAsync(map.JsRuntime, options);
            marker.Fox = fox;
            return marker;
        }



        private Icon CreateFoxIcon(DateTime lastUpdate)
        {
            return new Icon()
            {
                Url = getMarkerImage(lastUpdate),
                Anchor = new Point()
                {
                    X = 13,
                    Y = 13
                },
                Size = new Size()
                {
                    Width = 32,
                    Height = 32
                },
                ScaledSize = new Size()
                {
                    Width = 26,
                    Height = 26
                }
            };
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
            if (!isUpdatingLocation)
            {
                try
                {
                    isUpdatingLocation = true;
                    if (currentLocationMarker == null)
                    {
                        Console.WriteLine("Creating new location marker");
                        await CreateLocationMarker(lat, lon);
                        await CreateAccuracyCircle(lat, lon, accuracy);

                        await map.InteropObject.SetCenter(new LatLngLiteral(lon, lat));
                    }
                    else
                    {
                        Console.WriteLine("Updating location");
                        await currentLocationMarker.SetPosition(new LatLngLiteral(lon, lat));
                        await accuracyCircle.SetCenter(new LatLngLiteral(lon, lat));
                        await accuracyCircle.SetRadius(accuracy);
                    }
                }
                finally
                {
                    isUpdatingLocation = false;
                }
            }
        }


        private string getMarkerImage(DateTime lastUpdate)
        {
            double ageM = (DateTime.UtcNow - lastUpdate).TotalMinutes;

            if(ageM < 30)
            {
                return targetFresh;
            }
            else
            {
                return targetOld;
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
            foxUpdateTimer.Dispose();
            locationService.GeoLocationStateHasChanged -= LocationUpdated;
        }
    }


    class FoxMarker
    {
        public Marker Marker { get; set; }
        public WinchFox Fox { get; set; }
    }
}
