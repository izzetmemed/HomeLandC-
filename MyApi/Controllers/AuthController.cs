using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using MyApi.Controllers.SystemAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Twilio.Jwt.AccessToken;


namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserModel user = new UserModel();
        private readonly IConfiguration _configuration;
        public UserOpration userOpration=new UserOpration();
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration= configuration;
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
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

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

          
            return Token;
        }
        //[Authorize]
        // [HttpPost("refresh-token")]
        //public async Task<ActionResult<string>> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["Resp"];

        //    if (!user.RefreshToken.Equals(refreshToken))
        //    {
        //        return Unauthorized("Invalid Refresh Token.");
        //    }
        //    else if(user.TokenExpires < DateTime.Now)
        //    {
        //        return Unauthorized("Token expired.");
        //    }
        //    var data = userOpration.GetByName("Mizzat717").Data;
        //    string token = CreateToken(data);
        //    var newRefreshToken = GenerateRefreshToken();
        //    SetRefreshToken(newRefreshToken);

        //    return Ok(token);
        //}

        //private RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        Expires = DateTime.Now.AddMinutes(1),
        //        Created = DateTime.Now
        //    };

        //    return refreshToken;
        //}

        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires
        //    };
        //    Response.Cookies.Append("Resp", newRefreshToken.Token, cookieOptions);

        //    user.RefreshToken = newRefreshToken.Token;
        //    user.TokenCreated = newRefreshToken.Created;
        //    user.TokenExpires = newRefreshToken.Expires;
        //}
        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>()
           {
               new Claim(ClaimTypes.Name,user.UserName)
           };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("HomeLand:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var Token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials:cred
                ) ;
            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);

            return jwt;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
