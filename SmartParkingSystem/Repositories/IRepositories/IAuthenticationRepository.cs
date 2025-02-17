using SmartParkingSystem.Models;

namespace SmartParkingSystem.Repositories.IRepositories
{
    public interface IAuthenticationRepository
    {
        ApplicationUser AuthenticateUser(string email, string password);
        //ApplicationUser? AuthenticateUser(string email, string password);
        RegisterViewModel register(RegisterViewModel user);
    }
}
