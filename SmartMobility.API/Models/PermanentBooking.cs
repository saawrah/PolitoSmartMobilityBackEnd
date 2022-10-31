using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartMobility.API.Models
{
    [BsonIgnoreExtraElements]
    public class PermanentBooking : BasePermanent
    {
        //public ObjectId Id { get; set; }
        public OriginDestination origin_destination { get; set; }
        public string init_address { get; set; }
        public string final_address { get; set; }
    }
}
