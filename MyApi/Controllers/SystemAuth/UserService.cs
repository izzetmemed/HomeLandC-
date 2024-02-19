using System.Security.Claims;

namespace MyApi.Controllers.SystemAuth
{
    public class userService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public userService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
