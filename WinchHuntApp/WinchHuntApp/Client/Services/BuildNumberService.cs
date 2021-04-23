using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Client.Properties;

namespace WinchHuntApp.Client.Services
{
    public class BuildNumberService
    {

        public string ClientBuildNumber
        {
            get
            {
                return ClientBuild.Number;
            }
        }

        public BuildNumberService()
        {

        }


    }
}
