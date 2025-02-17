using SmartParkingSystem.Models;
using System.Threading.Tasks;

namespace SmartParkingSystem.Services
{
    public interface IUserService : IService<ApplicationUser>
    {
        Task<ApplicationUser> FindByUsernameAsync(string username);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
