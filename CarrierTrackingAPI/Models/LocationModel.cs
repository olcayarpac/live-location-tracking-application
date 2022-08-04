using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarrierTrackingAPI.Models
{
    [BsonIgnoreExtraElements]
    public class LocationModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("lattitude")]
        public string Lattitude { get; set; } 
        
        [BsonElement("longtitude")]
        public string Longtitude { get; set; }
        
        [BsonElement("lastUpdate")]
        public DateTime LastUpdate { get; set; } 
    }
}
