﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartMobility.API.Models
{
    [BsonIgnoreExtraElements]
    public class ActiveParking
    {
        public ObjectId Id { get; set; }

    }
}
