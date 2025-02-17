using SmartParkingSystem.Services.IServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories.IRepositories;
using IAuthenticationService = SmartParkingSystem.Services.IServices.IAuthenticationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SmartParkingSystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _authenticationRepository = authenticationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public RegisterViewModel register(RegisterViewModel user)
        {
            return _authenticationRepository.register(user);
        }

        public async Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties)
        {
            if (scheme == null)
            {
                scheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            }
            await context.SignInAsync(scheme, principal, properties);
        }
        public async Task<ApplicationUser?> LoginAsyncall(string email, string password)
        {
            var user = _authenticationRepository.AuthenticateUser(email, password);
            if (user == null)
                return null;

            // ✅ Manually set authentication cookie
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return user;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        public Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
        {
            if (scheme == null)
            {
                scheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            }
            return context.SignOutAsync(scheme, properties);
        }

        Task IAuthenticationService.LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        //    Task IAuthenticationService.LoginAsync(string email, string password)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
