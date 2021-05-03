using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Models
{
    public class UplinkPost
    {
        public List<WinchFox> Devices { get; set; } = new List<WinchFox>();

        /// <summary>
        /// Gets or sets the Hunter that receives the LoRa signals
        /// </summary>
        public WinchHunter Hunter { get; set; }
    }
}
