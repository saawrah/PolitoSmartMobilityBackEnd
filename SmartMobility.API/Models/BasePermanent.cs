namespace SmartMobility.API.Models
{
    public class BasePermanent
    {
        public DateTime init_date { get; set; }
        public DateTime final_date { get; set; }
        public string city { get; set; }
        public string vin { get; set; }


        public TimeSpan Duration => (final_date - init_date).Duration();
    }
}
