namespace SmartMobility.API.DTOs
{
    public class CDFChart
    {
        public double Duration { get; set; }
        public double Cdf { get; set; }

        public double BookingCdf { get; set; }
        public double ParkingCdf { get; set; }

    }
}
