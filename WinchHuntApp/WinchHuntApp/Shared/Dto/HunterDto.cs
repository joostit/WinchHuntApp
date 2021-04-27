using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Dto
{
    public class HunterDto
    {
        public bool HasResult
        {
            get
            {
                return Hunter != null;
            }
        }


        public WinchHunter Hunter { get; set; }

    }
}
