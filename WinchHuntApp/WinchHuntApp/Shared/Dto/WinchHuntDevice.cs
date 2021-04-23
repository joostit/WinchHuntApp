using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Dto
{
    public class WinchHuntDevice
    {
        /// <summary>
        /// Gets Device information
        /// </summary>
        public DeviceInfo Device { get; set; }

        /// <summary>
        /// Gets the time stamp of the last update that was received from the device
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
