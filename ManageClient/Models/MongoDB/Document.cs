using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Document
    {
     [BsonId]
     [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Username { get; set; } = null!;

        public string ImageName { get; set; } = null!;
        public string ImageUrl { get; set; } // ссылка на изображение

    }
}
