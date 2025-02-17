using System.Collections.Generic;

namespace SmartParkingSystem.Models
{
    public class ParkingLotListViewModel
    {
        public List<ParkingLotsDTO> ParkingLots { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchQuery { get; set; }
    }
}
