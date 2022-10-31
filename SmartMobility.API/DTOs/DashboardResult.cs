namespace SmartMobility.API.DTOs
{
    public class DashboardResult
    {

        public int ActiveBookingNo { get; set; }
        public int ActiveParkingNo { get; set; }
        public int PermanentBookingNo { get; set; }
        public int PermanentParkingNo { get; set; }

        public List<string> Cites { get; set; } = new List<string>();
        public DateTime StartCollectionDT { get; set; }
        public DateTime EndCollectionDT { get; set; }

        public List<NameValue> AvailableCarsInCities { get; set; } = new List<NameValue>();
        public List<NameValue> AvailableCarsInCitiesJan2017 { get; set; } = new List<NameValue>();


        //Charts Whitout Filter
        public List<ChartData> CarsChartDataPerDateAllCity { get; set; } = new List<ChartData>();

        public List<ChartData> CarsChartDataPerDateToronto { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerDateBerlin { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerDateFirenze { get; set; } = new List<ChartData>();


        public List<ChartData> CarsChartDataPerHourAllCity { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerHourToronto { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerHourBerlin { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerHourFirenze { get; set; } = new List<ChartData>();


        //Charts Whit Filter
        public List<ChartData> CarsChartDataPerDateAllCityFiltered { get; set; } = new List<ChartData>();

        public List<ChartData> CarsChartDataPerDateTorontoFiltered { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerDateBerlinFiltered { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerDateFirenzeFiltered { get; set; } = new List<ChartData>();


        public List<ChartData> CarsChartDataPerHourAllCityFiltered { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerHourTorontoFiltered { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerHourBerlinFiltered { get; set; } = new List<ChartData>();
        public List<ChartData> CarsChartDataPerHourFirenzeFiltered { get; set; } = new List<ChartData>();



        public List<StatisticChart> StatisticChartDataPerDateAllCityFiltered { get; set; } = new List<StatisticChart>();
        public List<StatisticChart> StatisticChartDataPerDateTorontoFiltered { get; set; } = new List<StatisticChart>();
        public List<StatisticChart> StatisticChartDataPerDateBerlinFiltered { get; set; } = new List<StatisticChart>();
        public List<StatisticChart> StatisticChartDataPerDateFirenzeFiltered { get; set; } = new List<StatisticChart>();


        //Map

        public List<HeatMapData> WeekendHeatMapDatas { get; set; } = new List<HeatMapData>();
        public List<HeatMapData> WeekHeatMapDatas { get; set; } = new List<HeatMapData>();


        public List<CDFChart> cdfChart { get; set; } = new List<CDFChart>();


        public List<CDFChart> cdfChartTorino { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartBerlin { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartFirenze { get; set; } = new List<CDFChart>();

        public List<CDFChart> cdfChartTorinoFiltered { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartBerlinFiltered { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartFirenzeFiltered { get; set; } = new List<CDFChart>();


        public List<CDFChart> cdfChartSunday { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartMonday { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartTuesday { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartWednesday { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartThursday { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartFriday { get; set; } = new List<CDFChart>();
        public List<CDFChart> cdfChartSaturday { get; set; } = new List<CDFChart>();


    }
}
