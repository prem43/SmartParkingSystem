namespace SmartParkingSystem.Models
{
    public class ParkingLotsDTO
    {
        public int LotID { get; set; }        // Maps to the 'LotID' column (Primary Key)
        public string Name { get; set; }      // Maps to the 'Name' column
        public string Location { get; set; }  // Maps to the 'Location' column
        public int TotalSlots { get; set; }   // Maps to the 'TotalSlots' column
        public int AvailableSlots { get; set; } // Maps to the 'AvailableSlots' column
        public decimal Price { get; set; }    // Maps to the 'Price' column
                                              // Ensure proper default DateTime values
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

