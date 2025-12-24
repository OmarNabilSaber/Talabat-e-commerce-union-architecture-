using Dev.Talabat.Application.abstruction;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dev.Talabat.APIs.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private IHttpContextAccessor? _httpContextAccessor;
        public string UserId { get; }
        public LoggedInUserService(IHttpContextAccessor? httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid)!;
        }


    }
}
