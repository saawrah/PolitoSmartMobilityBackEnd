using SmartMobility.API.Models;

namespace SmartMobility.API.DTOs
{
    public class SaveObj
    {
        public List<PermanentBooking> PermanentBookings { get; set; }
        public List<PermanentParking> PermanentParkings { get; set; }
    }
}
