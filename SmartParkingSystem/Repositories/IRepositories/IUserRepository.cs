using SmartParkingSystem.Models;
using System.Threading.Tasks;

namespace SmartParkingSystem.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> FindByUsernameAsync(string username);
    }
}
