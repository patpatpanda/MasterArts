using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; }
        public string UserId { get; set; } // Referens till IdentityUser

        // Navigeringsegenskap till IdentityUser
        public virtual IdentityUser User { get; set; }
    }

}
