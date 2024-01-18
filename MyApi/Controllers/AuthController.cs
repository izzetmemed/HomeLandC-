using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserModel user = new UserModel();
        private readonly IConfiguration _configuration;
        public UserOpration userOpration=new UserOpration();
        public AuthController(IConfiguration configuration)
        {
            _configuration= configuration;
        }

       

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO userDTO)
        {
            var data = userOpration.GetByName(userDTO.UserName).Data;
            if (data ==null)
            {
                return BadRequest("UserName doesnt exist");
            }
            if(!VerifyPasswordHash(userDTO.Password,data.PasswordHash,data.PasswordSalt))
            {
                return BadRequest("Password wrong");
            }
            string Token = CreateToken(data);
            return Token;
        }

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>()
           {
               new Claim(ClaimTypes.Name,user.UserName)
           };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("HomeLand:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var Token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials:cred
                ) ;
            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);

            return jwt;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
