using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Server.Data.Db;
using WinchHuntApp.Server.Models;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public class FoxService : IFoxService
    {

        private List<DbFox> Foxes { get; set; } = new List<DbFox>();

        private readonly WinchHuntContext context;

        public FoxService(WinchHuntContext ctx)
        {
            context = ctx;
        }


        public async Task<IEnumerable<WinchFox>> GetFoxesAsync()
        {
            List<WinchFox> retVal = new List<WinchFox>();

            await Task.Run(() =>
            {
                foreach (var fox in Foxes)
                {
                    retVal.Add(CreateFoxDto(fox));
                }
            });

            return retVal;
        }

        private WinchFox CreateFoxDto(DbFox fox)
        {
            WinchFox dto = new WinchFox();

            dto.Gps.Latitude = fox.Latitude;
            dto.Gps.Longitude = fox.Longitude;
            dto.Gps.Satellites = fox.Satellites;
            dto.Gps.Speed = fox.Speed;
            dto.Gps.Altitude = fox.Altitude;
            dto.Gps.Hdop = fox.Hdop;
            dto.Gps.HasFix = fox.HasFix;
            dto.Device.Name = fox.Name;
            dto.Device.Id = fox.DeviceId;
            dto.Device.Hardware = fox.Hardware;
            dto.Device.Version = fox.Version;
            dto.Device.DeviceType = fox.DeviceType;
            dto.LastUpdate = fox.LastUpdate;
            dto.LastRssi = fox.LastRssi;

            return dto;
        }


        public async Task ProcessFoxUpdateAsync(UplinkPost post)
        {

            List<DbFox> foxesToRemove = new List<DbFox>();

            foreach (var existing in Foxes)
            {
                bool containsInUpdate = post.Devices.Where(d => existing.DeviceId.Equals(d.Device.Id)).SingleOrDefault() != null;

                if (!containsInUpdate)
                {
                    foxesToRemove.Add(existing);
                }
            }


            foreach (var teRemove in foxesToRemove)
            {
                Foxes.Remove(teRemove);
            }


            foreach (var update in post.Devices)
            {
                DbFox existing = Foxes.Where(f => f.DeviceId.Equals(update.Device.Id)).SingleOrDefault();

                if (existing == null)
                {
                    existing = new DbFox();
                    Foxes.Add(existing);
                }

                UpdateExistingFox(existing, update);
            }

        }

        private void UpdateExistingFox(DbFox existing, WinchFox update)
        {
            existing.Latitude = update.Gps.Latitude;
            existing.Longitude = update.Gps.Longitude;
            existing.Satellites = update.Gps.Satellites;
            existing.Speed = update.Gps.Speed;
            existing.Altitude = update.Gps.Altitude;
            existing.Hdop = update.Gps.Hdop;
            existing.HasFix = update.Gps.HasFix;
            existing.Name = update.Device.Name;
            existing.DeviceId = update.Device.Id;
            existing.Hardware = update.Device.Hardware;
            existing.Version = update.Device.Version;
            existing.DeviceType = update.Device.DeviceType;
            existing.LastUpdate = update.LastUpdate;
            existing.LastRssi = update.LastRssi;
        }

        public async Task<WinchFox> GetFoxAsync(string id)
        {
            var dbObject = Foxes.Where(f => f.DeviceId.Equals(id)).FirstOrDefault();

            if (dbObject != null)
            {
                return CreateFoxDto(dbObject);
            }
            else
            {
                return null;
            }
        }


        public List<WinchFox> GetFakeFoxes()
        {
            List<WinchFox> foxes = new List<WinchFox>()
            {
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.271884,
                        Longitude = 6.882393
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeA",
                        Id = "112233"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.278816,
                        Longitude = 6.899221,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeB",
                        Id = "AABBCC"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.278784,
                        Longitude = 6.899211,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeC",
                        Id = "BBCCDD"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.275612,
                        Longitude = 6.894939,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "FakeD",
                        Id = "223344"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                },
                new WinchFox()
                {
                    Gps = new GpsInfo()
                    {
                        Altitude = 35,
                        HasFix = true,
                        Hdop = 1,
                        Satellites = 7,
                        Speed = 0.5,
                        Latitude = 52.270090,
                        Longitude = 6.876612,
                    },
                    Device = new DeviceInfo()
                    {
                        DeviceType = DeviceTypes.Fox,
                        Name = "Target",
                        Id = "010101"
                    },
                    LastRssi = -50,
                    LastUpdate = DateTime.UtcNow
                }
            };


            return foxes;
        }

    }
}
