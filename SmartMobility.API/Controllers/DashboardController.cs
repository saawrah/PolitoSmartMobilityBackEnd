using Common.Tool.JsonTools;
using MathNet.Numerics.Distributions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartMobility.API.Data;
using SmartMobility.API.DTOs;
using SmartMobility.API.Models;
using SmartMobility.API.Tools;
using System.Linq;

namespace SmartMobility.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        MongoDbContext _mcontext;
        private readonly ILogger<DashboardController> _logger;
        private IWebHostEnvironment _environment;
        public DashboardController(ILogger<DashboardController> logger, MongoDbContext context, IWebHostEnvironment environment)
        {
            _logger = logger;
            _mcontext = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<DashboardResult> Get()
        {
            var lstMyCity = new List<string>();
            lstMyCity.Add("Toronto");
            lstMyCity.Add("Firenze");
            lstMyCity.Add("Berlin");

            var res = new DashboardResult();
            //res.ActiveBookingNo = await _mcontext.ActiveBookings.AsQueryable().CountAsync();
            //res.ActiveParkingNo = await _mcontext.ActiveParkings.AsQueryable().CountAsync();
            //res.PermanentBookingNo = await _mcontext.PermanentBookings.AsQueryable().CountAsync();
            //res.PermanentParkingNo = await _mcontext.PermanentParkings.AsQueryable().CountAsync();

            //res.Cites = await _mcontext.ActiveBookings.AsQueryable().Select(x => x.city).Distinct().ToListAsync();

            //res.StartCollectionDT = await _mcontext.PermanentBookings.AsQueryable().Select(x => x.init_date).MinAsync(x => x);
            //res.EndCollectionDT = await _mcontext.PermanentBookings.AsQueryable().Select(x => x.final_date).MaxAsync(x => x);

            //var lstParkedCarsInCities = await _mcontext.PermanentParkings.AsQueryable().Where(x => lstMyCity.Contains(x.city)).ToListAsync();
            //res.AvailableCarsInCities = lstParkedCarsInCities.GroupBy(x => x.city).Select(x => new NameValue
            //{
            //    Name = x.Key,
            //    Value = x.Select(a => a.vin).Distinct().Count(),
            //}).ToList();

            //var lstBookedCarsInCities = await _mcontext.PermanentBookings.AsQueryable().Where(x => lstMyCity.Contains(x.city) && x.init_date > new DateTime(2016, 12, 31) && x.init_date < new DateTime(2017, 2, 1)).ToListAsync();
            //res.AvailableCarsInCitiesJan2017 = lstBookedCarsInCities.GroupBy(x => x.city).Select(x => new NameValue
            //{
            //    Name = x.Key,
            //    Value = x.Count(),
            //}).ToList();

            //return res;

            var saveObj = new SaveObj();

            //saveObj.PermanentBookings = await _mcontext.PermanentBookings.AsQueryable().Where(x => lstMyCity.Contains(x.city) && x.init_date > new DateTime(2017, 11, 30) && x.init_date < new DateTime(2018, 3, 1)).ToListAsync();
            //saveObj.PermanentParkings = await _mcontext.PermanentParkings.AsQueryable().Where(x => lstMyCity.Contains(x.city) && x.init_date > new DateTime(2017, 11, 30) && x.init_date < new DateTime(2018, 3, 1)).ToListAsync();

            // JsonTools.WriteJsonFile(@"E:\Active Project\Sara\SmartMobility\SavedData\data.json", saveObj);
            var fileName = Path.Combine(_environment.ContentRootPath, "SavedData", "data.json");



            saveObj = JsonTools.ReadJsonFile<SaveObj>(fileName);

            var permanentBookingsFilterData = FilterPermanentBookingData(saveObj.PermanentBookings);
            var permanentParkingsFilterData = FilterPermanentParkingData(saveObj.PermanentParkings);



            //CDF

            res.cdfChart = permanentBookingsFilterData.Where(x => x.city == "Firenze").Select(x => new CDFChart
            {
                Duration = x.Duration.TotalMinutes,
                Cdf = CumulativeFunction.F(x.Duration.TotalMinutes),
            }).ToList();



            // res.cdfChart= res.cdfChart.Where(x=>x.Cdf1!=0 && x.Cdf2!=0).ToList();


            //// All City Date
            res.CarsChartDataPerDateAllCity = (from book in GetGroupDataPerDate(saveObj.PermanentBookings)
                                               join park in GetGroupDataPerDate(saveObj.PermanentParkings) on book.DateTime equals park.DateTime
                                               select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            res.CarsChartDataPerDateAllCityFiltered = (from book in GetGroupDataPerDate(permanentBookingsFilterData)
                                                       join park in GetGroupDataPerDate(permanentParkingsFilterData) on book.DateTime equals park.DateTime
                                                       select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            res.StatisticChartDataPerDateAllCityFiltered = (from book in GetStatisticDataPerDate(permanentBookingsFilterData)
                                                            join park in GetStatisticDataPerDate(permanentParkingsFilterData) on book.DateTime equals park.DateTime
                                                            select new StatisticChart
                                                            {
                                                                DateTime = book.DateTime,
                                                                BookedAverage = book.Average,
                                                                ParkedAverage = park.Average,
                                                                BookedMedian = book.Median,
                                                                ParkedMedian = park.Median,
                                                                ParkedStdDev = park.StdDev,
                                                                BookedStdDev = book.StdDev,
                                                                BookedPercentile = book.Percentile,
                                                                ParkedPercentile = park.Percentile,
                                                            }).ToList();



            //// Firenze Date

            res.CarsChartDataPerDateFirenze = (from book in GetGroupDataPerDate(saveObj.PermanentParkings, "Firenze")
                                               join park in GetGroupDataPerDate(saveObj.PermanentParkings, "Firenze") on book.DateTime equals park.DateTime
                                               select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();
            res.CarsChartDataPerDateFirenzeFiltered = (from book in GetGroupDataPerDate(permanentBookingsFilterData, "Firenze")
                                                       join park in GetGroupDataPerDate(permanentParkingsFilterData, "Firenze") on book.DateTime equals park.DateTime
                                                       select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();


            res.StatisticChartDataPerDateFirenzeFiltered = (from book in GetStatisticDataPerDate(permanentBookingsFilterData, "Firenze")
                                                            join park in GetStatisticDataPerDate(permanentParkingsFilterData, "Firenze") on book.DateTime equals park.DateTime
                                                            select new StatisticChart
                                                            {
                                                                DateTime = book.DateTime,
                                                                BookedAverage = book.Average,
                                                                ParkedAverage = park.Average,
                                                                BookedMedian = book.Median,
                                                                ParkedMedian = park.Median,
                                                                ParkedStdDev = park.StdDev,
                                                                BookedStdDev = book.StdDev,
                                                                BookedPercentile = book.Percentile,
                                                                ParkedPercentile = park.Percentile,
                                                            }).ToList();


            //// Toronto Date
            res.CarsChartDataPerDateToronto = (from book in GetGroupDataPerDate(saveObj.PermanentBookings, "Toronto")
                                               join park in GetGroupDataPerDate(saveObj.PermanentParkings, "Toronto") on book.DateTime equals park.DateTime
                                               select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();
            res.CarsChartDataPerDateTorontoFiltered = (from book in GetGroupDataPerDate(permanentBookingsFilterData, "Toronto")
                                                       join park in GetGroupDataPerDate(permanentParkingsFilterData, "Toronto") on book.DateTime equals park.DateTime
                                                       select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            res.StatisticChartDataPerDateTorontoFiltered = (from book in GetStatisticDataPerDate(permanentBookingsFilterData, "Toronto")
                                                            join park in GetStatisticDataPerDate(permanentParkingsFilterData, "Toronto") on book.DateTime equals park.DateTime
                                                            select new StatisticChart
                                                            {
                                                                DateTime = book.DateTime,
                                                                BookedAverage = book.Average,
                                                                ParkedAverage = park.Average,
                                                                BookedMedian = book.Median,
                                                                ParkedMedian = park.Median,
                                                                ParkedStdDev = park.StdDev,
                                                                BookedStdDev = book.StdDev,
                                                                BookedPercentile = book.Percentile,
                                                                ParkedPercentile = park.Percentile,
                                                            }).ToList();

            //// Berlin Date


            res.CarsChartDataPerDateBerlin = (from book in GetGroupDataPerDate(saveObj.PermanentBookings, "Berlin")
                                              join park in GetGroupDataPerDate(saveObj.PermanentParkings, "Berlin") on book.DateTime equals park.DateTime
                                              select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();
            res.CarsChartDataPerDateBerlinFiltered = (from book in GetGroupDataPerDate(permanentBookingsFilterData, "Berlin")
                                                      join park in GetGroupDataPerDate(permanentParkingsFilterData, "Berlin") on book.DateTime equals park.DateTime
                                                      select new ChartData { DateTime = book.DateTime, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            res.StatisticChartDataPerDateBerlinFiltered = (from book in GetStatisticDataPerDate(permanentBookingsFilterData, "Berlin")
                                                           join park in GetStatisticDataPerDate(permanentParkingsFilterData, "Berlin") on book.DateTime equals park.DateTime
                                                           select new StatisticChart
                                                           {
                                                               DateTime = book.DateTime,
                                                               BookedAverage = book.Average,
                                                               ParkedAverage = park.Average,
                                                               BookedMedian = book.Median,
                                                               ParkedMedian = park.Median,
                                                               ParkedStdDev = park.StdDev,
                                                               BookedStdDev = book.StdDev,
                                                               BookedPercentile = book.Percentile,
                                                               ParkedPercentile = park.Percentile,
                                                           }).ToList();



            //// All City Hour
            res.CarsChartDataPerHourAllCity = (from book in GetGroupDataPerHour(saveObj.PermanentBookings)
                                               join park in GetGroupDataPerHour(saveObj.PermanentParkings) on book.Hour equals park.Hour
                                               select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();
            res.CarsChartDataPerHourAllCityFiltered = (from book in GetGroupDataPerHour(permanentBookingsFilterData)
                                                       join park in GetGroupDataPerHour(permanentParkingsFilterData) on book.Hour equals park.Hour
                                                       select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();


            //// Firenze Hour

            res.CarsChartDataPerHourFirenze = (from book in GetGroupDataPerHour(saveObj.PermanentParkings, "Firenze")
                                               join park in GetGroupDataPerHour(saveObj.PermanentParkings, "Firenze") on book.Hour equals park.Hour
                                               select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            res.CarsChartDataPerHourFirenzeFiltered = (from book in GetGroupDataPerHour(permanentBookingsFilterData, "Firenze")
                                                       join park in GetGroupDataPerHour(permanentParkingsFilterData, "Firenze") on book.Hour equals park.Hour
                                                       select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();


            //// Toronto Hour
            res.CarsChartDataPerHourToronto = (from book in GetGroupDataPerHour(saveObj.PermanentBookings, "Toronto")
                                               join park in GetGroupDataPerHour(saveObj.PermanentParkings, "Toronto") on book.Hour equals park.Hour
                                               select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            res.CarsChartDataPerHourTorontoFiltered = (from book in GetGroupDataPerHour(permanentBookingsFilterData, "Toronto")
                                                       join park in GetGroupDataPerHour(permanentParkingsFilterData, "Toronto") on book.Hour equals park.Hour
                                                       select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();

            //// Berlin Hour

            res.CarsChartDataPerHourBerlin = (from book in GetGroupDataPerHour(saveObj.PermanentBookings, "Berlin")
                                              join park in GetGroupDataPerHour(saveObj.PermanentParkings, "Berlin") on book.Hour equals park.Hour
                                              select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();
            res.CarsChartDataPerHourBerlinFiltered = (from book in GetGroupDataPerHour(permanentBookingsFilterData, "Berlin")
                                                      join park in GetGroupDataPerHour(permanentParkingsFilterData, "Berlin") on book.Hour equals park.Hour
                                                      select new ChartData { Hour = book.Hour, BookingCount = book.TotalCount, ParkedCount = park.TotalCount }).ToList();



            //Map
            res.WeekendHeatMapDatas = saveObj.PermanentBookings.Where(x => (x.init_date.DayOfWeek == DayOfWeek.Sunday || x.init_date.DayOfWeek == DayOfWeek.Saturday) && x.city == "Firenze").Select(x => new HeatMapData
            {
                Longitude = x.origin_destination.coordinates[1].First(),
                Latitude = x.origin_destination.coordinates[1].Last()
            }).ToList();

          


            res.WeekHeatMapDatas = saveObj.PermanentBookings.Where(x => (x.init_date.DayOfWeek == DayOfWeek.Monday || x.init_date.DayOfWeek == DayOfWeek.Wednesday) && x.city == "Firenze").Select(x => new HeatMapData
            {
                Longitude = x.origin_destination.coordinates[1].First(),
                Latitude = x.origin_destination.coordinates[1].Last()
            }).ToList();


            return res;
        }


        private List<ChartData> GetGroupDataPerDate<T>(List<T> lstData, string? cityName = null) where T : BasePermanent
        {
            return lstData.Where(x => cityName == null ? true : x.city == cityName).GroupBy(x => x.init_date.Date).Select(x => new ChartData
            {
                TotalCount = x.Count(),
                DateTime = x.Key.ToShortDateString(),
            }).ToList();
        }




        private List<StatisticChart> GetStatisticDataPerDate<T>(List<T> lstData, string? cityName = null) where T : BasePermanent
        {
            return lstData.Where(x => cityName == null ? true : x.city == cityName).GroupBy(x => x.init_date.Date).Select(x => new StatisticChart
            {
                Average = x.Select(a => a.Duration.TotalMinutes).Average(),
                Median = (double)x.Median(a => a.Duration.TotalMinutes),
                StdDev = x.Select(a => a.Duration.TotalMinutes).StdDev(),
                Percentile = x.Select(a => a.Duration.TotalMinutes).Percentile(0.85),
                DateTime = x.Key.ToShortDateString(),
            }).ToList();
        }


        private List<ChartData> GetGroupDataPerHour<T>(List<T> lstData, string? cityName = null) where T : BasePermanent
        {
            return lstData.Where(x => cityName == null ? true : x.city == cityName).GroupBy(x => x.init_date.Hour).Select(x => new ChartData
            {
                TotalCount = x.Count(),
                Hour = x.Key
            }).ToList();
        }


        private int FindShortestTime(List<PermanentBooking> lstData)
        {
            var a = lstData.Where(x =>
              x.origin_destination.coordinates[0].Sum() != x.origin_destination.coordinates[1].Sum()
            ).OrderBy(x => x.Duration).FirstOrDefault();

            return 0;
        }

        private List<PermanentBooking> FilterPermanentBookingData(List<PermanentBooking> lstData)
        {
            return lstData.Where(x =>
            x.origin_destination.coordinates[0].Sum() != x.origin_destination.coordinates[1].Sum()
            ).ToList();
        }

        private List<PermanentParking> FilterPermanentParkingData(List<PermanentParking> lstData)
        {
            return lstData.Where(x =>
            x.Duration > new TimeSpan(0, 1, 0)
            ).ToList();
        }
    }
}
