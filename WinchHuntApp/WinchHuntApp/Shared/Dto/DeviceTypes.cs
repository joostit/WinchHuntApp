using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Dto
{
    /// <summary>
    /// Defines different WinchHunt device types
    /// </summary>
    public enum DeviceTypes
    {
        /// <summary>
        /// Indicates an unknown device or an error
        /// </summary>
        Unknown,

        /// <summary>
        /// A Hunter
        /// </summary>
        Hunter,

        /// <summary>
        /// A Fox
        /// </summary>
        Fox
    }
}
