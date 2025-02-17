using SmartParkingSystem.Models;

namespace SmartParkingSystem.Repositories.IRepositories
{
    public interface IParkingLotsRepository
    {
        List<ParkingLotsDTO> Getall();
    }
}
