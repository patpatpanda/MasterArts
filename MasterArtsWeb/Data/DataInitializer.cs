using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MasterArtsWeb.Data
{
    public class DataInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MyDbContext _dbContext;

        public DataInitializer(MyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public void SeedData()
        {
            _dbContext.Database.Migrate();
            
            SeedUsers();
        }


       


        private void SeedUsers()
        {
            // Lägg till användare med användarnamn och lösenord
            AddUserIfNotExists("nils-emil1337@hotmail.se", "Hejsan123!");
            AddUserIfNotExists("ogagabogabou@live.se", "Hejsan123!");
        }

        private void AddRoleIfNotExisting(string roleName)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                _dbContext.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
                _dbContext.SaveChanges();
            }
        }

        private void AddUserIfNotExists(string userName, string password)
        {
            // Kontrollera om användaren redan finns
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            // Skapa en ny användare
            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };

            // Lägg till användaren i databasen
            _userManager.CreateAsync(user, password).Wait();
        }
    }
}

