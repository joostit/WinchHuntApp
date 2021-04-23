using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Dto
{
    public class DeviceInfo
    {

        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The device ID
        /// </summary>
        public string Id { get; set; }

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
        /// Constructor
        /// </summary>
        public DeviceInfo()
        {

        }
    }
}
