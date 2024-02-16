using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterArtsLibrary.ViewModels;

namespace MasterArtsLibrary.Services
{
    public interface IUser
    {
        public IEnumerable<IdentityUser> GetUsers();

        public IEnumerable<UserViewModel> GenerateUserInfoAndRoles();
    }
}
