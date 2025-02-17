using SmartParkingSystem.Models;

namespace SmartParkingSystem.Repositories.IRepositories
{
    public interface IAuthenticationRepository
    {
       RegisterViewModel register(RegisterViewModel user);
    }
}
