using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Data.Db
{
    public class DbFox
    {


        /// <summary>
        /// Database key
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Gets the latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets the longitute
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets the number of satellites currently being tracked
        /// </summary>
        public int Satellites { get; set; }

        /// <summary>
        /// Gets the speed (in km/h)
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Gets the altitude (in meters)
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// Gets the GPS HDOP value
        /// </summary>
        public double Hdop { get; set; }

        /// <summary>
        /// Gets whether the GPS receiver has a valid fix
        /// </summary>
        public bool HasFix { get; set; }

        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The device ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// The device Hardware
        /// </summary>
        public string Hardware { get; set; }

        /// <summary>
        /// The device Version
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The type of device
        /// </summary>
        public DeviceTypes DeviceType { get; set; }

        /// <summary>
        /// Gets the time stamp of the last update that was received from the device
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the RSSI value for the last transmission that was received
        /// </summary>
        public int LastRssi { get; set; }

    }
}
