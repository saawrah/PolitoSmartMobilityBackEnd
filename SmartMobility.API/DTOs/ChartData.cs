namespace SmartMobility.API.DTOs
{
    public class ChartData
    {
        public string DateTime { get; set; }
        public int Hour { get; set; }
        public int ParkedCount { get; set; }
        public int BookingCount { get; set; }

        public int TotalCount { get; set; }
    }
}
