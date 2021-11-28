using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Dto
{
    public class ResultInfo
    {

        public bool IsSuccess { get; set; }

        public string Message { get; set; } = "";

        public static ResultInfo Success()
        {
            return new ResultInfo()
            {
                IsSuccess = true,
                Message = String.Empty
            };
        }


        public static ResultInfo Error(string msg)
        {
            return new ResultInfo()
            {
                IsSuccess = false,
                Message = msg
            };
        }
    }
}
