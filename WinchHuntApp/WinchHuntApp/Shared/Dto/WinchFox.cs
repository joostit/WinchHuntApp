using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Dto
{
    public class WinchFox : WinchHuntDevice
    {


        public WinchFox()
        {
            Gps = new GpsInfo();
        }

        /// <summary>
        /// Gets last known GPS state information
        /// </summary>
        public GpsInfo Gps { get; set; } = new GpsInfo();


        /// <summary>
        /// Gets the last RSSI from the device
        /// </summary>
        public int LastRssi { get; set; }

    }
}
