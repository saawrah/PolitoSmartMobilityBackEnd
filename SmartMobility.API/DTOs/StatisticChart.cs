namespace SmartMobility.API.DTOs
{
    public class StatisticChart
    {
        public string DateTime { get; set; }
        public double Average { get; set; }
        public double BookedAverage { get; set; }
        public double ParkedAverage { get; set; }

        public double  Median { get; set; }
        public double BookedMedian { get; set; }
        public double ParkedMedian { get; set; }

        public double StdDev { get; set; }
        public double BookedStdDev { get; set; }
        public double ParkedStdDev { get; set; }


        public double Percentile { get; set; }
        public double BookedPercentile { get; set; }
        public double ParkedPercentile { get; set; }
    }
}
