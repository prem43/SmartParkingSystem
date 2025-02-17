using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories.IRepositories;
using SmartParkingSystem.Services.IServices;

namespace SmartParkingSystem.Services
{
    public class ParkingLotsService : IParkingLotsService
    {
        private readonly IParkingLotsRepository _parkingLotsRepository;

        public ParkingLotsService(IParkingLotsRepository parkingLotsRepository)
        {

            _parkingLotsRepository= parkingLotsRepository; 
        }
        public List<ParkingLotsDTO> Getall()
        {
            return _parkingLotsRepository.Getall();
        }
    }
}
