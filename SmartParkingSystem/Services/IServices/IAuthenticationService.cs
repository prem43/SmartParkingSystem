using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SmartParkingSystem.Models;

namespace SmartParkingSystem.Services.IServices
{
    public interface IAuthenticationService
    {
        RegisterViewModel register(RegisterViewModel user);
        Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties);
        Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties);
    }
}
