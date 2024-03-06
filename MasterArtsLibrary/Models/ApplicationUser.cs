using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomerNumber { get; set; }
    }
}
