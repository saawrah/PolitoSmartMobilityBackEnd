namespace SmartMobility.API.Models
{
    public class OriginDestination
    {
        public string type { get; set; }
        public List<List<double>> coordinates { get; set; }
    }
}
