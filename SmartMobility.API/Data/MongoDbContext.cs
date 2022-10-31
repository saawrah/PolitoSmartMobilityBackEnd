using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using SmartMobility.API.Models;

namespace SmartMobility.API.Data
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; set; }
        public MongoClient Client { get; set; }
        public CarSharingMongoDatabaseSettings DbSettings { get; set; }
        public IMongoCollection<ActiveBooking> ActiveBookings { get; set; }
        public IMongoCollection<ActiveParking> ActiveParkings { get; set; }
        public IMongoCollection<PermanentBooking> PermanentBookings { get; set; }
        public IMongoCollection<PermanentParking> PermanentParkings { get; set; }

        public MongoDbContext(IOptions<CarSharingMongoDatabaseSettings> settings)
        {
            DbSettings = settings.Value;
            var mongoConnectionUrl = new MongoUrl(DbSettings.ConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            mongoClientSettings.VerifySslCertificate = false;
            mongoClientSettings.ClusterConfigurator = cb =>
            {
                // This will print the executed command to the console
                //cb.Subscribe<CommandStartedEvent>(e =>
                //{
                //    Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
                //});

                cb.Subscribe<CommandStartedEvent>(x =>
                {
                    var queryDocument = x.Command;
                    if(queryDocument != null)
                    {
                        var a = 0;
                    }
                });

            };

            Client = new MongoClient(mongoClientSettings);
            Database = Client.GetDatabase(settings.Value.DatabaseName);
            ActiveBookings = Database.GetCollection<ActiveBooking>(DbSettings.ActiveBookingsCollectionName);
            ActiveParkings = Database.GetCollection<ActiveParking>(DbSettings.ActiveParkingsCollectionName);
            PermanentBookings = Database.GetCollection<PermanentBooking>(DbSettings.PermanentBookingsCollectionName);
            PermanentParkings = Database.GetCollection<PermanentParking>(DbSettings.PermanentParkingsCollectionName);
        }
    }
}
