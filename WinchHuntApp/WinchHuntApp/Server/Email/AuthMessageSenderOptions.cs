using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinchHuntApp.Server.Email
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string SendGridFromAddress { get; set; }
    }
}
