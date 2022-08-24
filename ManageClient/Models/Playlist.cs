using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Playlist
    {
     [BsonId]
     [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;

       

    }
}
