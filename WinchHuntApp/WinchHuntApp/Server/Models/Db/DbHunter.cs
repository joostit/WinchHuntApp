using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Data.Db
{
    public class DbHunter
    {

        /// <summary>
        /// Database key
        /// </summary>
        public int Id { get; set; }


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


    }
}
