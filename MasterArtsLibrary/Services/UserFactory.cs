using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public class UserFactory : IUser
    {

        public UserFactory(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }
        private readonly UserManager<IdentityUser> _userManager;




        public IEnumerable<IdentityUser> GetUsers()
        {
            var customers = _userManager.Users;
            return customers;
        }



        public IEnumerable<UserViewModel> GenerateUserInfoAndRoles()
        {
            var UserInfo = _userManager.Users.Select(c => new UserViewModel
            {
                Id = c.Id,
                Email = c.Email,
                UserName = c.UserName,
                Role = _userManager.GetRolesAsync(c).Result
            }).ToList();

            return UserInfo;
        }
    }
}
