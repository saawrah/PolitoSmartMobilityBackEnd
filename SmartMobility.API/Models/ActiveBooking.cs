using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SmartMobility.API.Models
{
    [BsonIgnoreExtraElements]
    public class ActiveBooking
    {
        public ObjectId Id { get; set; }
        public string city { get; set; }
        public int init_fuel { get; set; }

    }
}
