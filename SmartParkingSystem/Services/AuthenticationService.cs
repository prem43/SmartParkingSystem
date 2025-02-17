using SmartParkingSystem.Services.IServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories.IRepositories;
using IAuthenticationService = SmartParkingSystem.Services.IServices.IAuthenticationService;

namespace SmartParkingSystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
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

        public Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
        {
            if (scheme == null)
            {
                scheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            }
            return context.SignOutAsync(scheme, properties);
        }
    }
}
