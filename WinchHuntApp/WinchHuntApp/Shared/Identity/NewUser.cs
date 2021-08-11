using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinchHuntApp.Shared.Identity
{
    public class NewUser
    {
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

    }
}
