using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Utils.Http
{
    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Header()
        {

        }

        public Header(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
