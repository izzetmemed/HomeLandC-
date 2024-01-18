using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Contexts;
using Model.Models; // Ensure this is the correct namespace
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class MiddlewareRegister
    {
        private static UserModelAccess _userModelAccess=new UserModelAccess();

        public static async Task SeedMembershipAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {

                var middlewareRegister = scope.ServiceProvider.GetService<MyDbContext>();
                if (!_userModelAccess.GetAll().Any())
            {
                    IConfiguration configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .Build();
                    string password = configuration["PasswordLogin:Login"];
                    var userDTO = new UserDTO
                {
                    Password = password,
                    UserName = "Mizzat717",
                   
                };

                CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var newUser = new UserModel
                {
                    UserName = userDTO.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = "Izzet",
                    LastName = "Memmedov",
                    EmailConfirmed = true
                };

                _userModelAccess.Add(newUser);
            }
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
