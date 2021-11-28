using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Client.Utils.Http
{
    public class HeaderList : List<Header>
    {
        public HeaderList()
        {

        }

        public HeaderList(IEnumerable<Header> headers)
            : base(headers)
        {

        }

        public void Add(string name, string value)
        {
            base.Add(new Header(name, value));
        }
    }
}
