using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartParkingSystem.Services
{
    public class UserService : Service<ApplicationUser>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager) : base(userRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByUsernameAsync(string username)
        {
            return await _userRepository.FindByUsernameAsync(username);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }
    }
}
