using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }


        public string UserName { get; set; } = null!;


        public string Email { get; set; } = null!;




        public IList<string> Role { get; set; } = null!;

    }
}
