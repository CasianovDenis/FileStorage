using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MongoDBServices 
    {
        

        IGridFSBucket gridFS;   // файловое хранилище
        IMongoCollection<Document> documents; // коллекция в базе данных

        public MongoDBServices()
        {
            // строка подключения
            //string connectionString = "mongodb://localhost:27017/ManageClient";
            string connectionString = "mongodb+srv://AngelBenny:alibaba321@manageproject.xl5mycl.mongodb.net/?retryWrites=true&w=majority";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            // IMongoDatabase database = client.GetDatabase(connection.DatabaseName);

            IMongoDatabase database = client.GetDatabase("ManageProject");
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);

            // обращаемся к коллекции Products
           // playlist = database.GetCollection<Document>("collectiondata");
            documents = database.GetCollection<Document>("mytable");
        }
        
        // получаем один документ по id
        public  Document Get(string username)
        {
            return documents.Find(new BsonDocument("Username", new string(username))).FirstOrDefault();
        }

        public IEnumerable<Document> GetAll(string username)
        {
            var get = documents.Find(new BsonDocument("Username", new string(username))).ToList();
            return get;
        }
        // добавление документа
        public async Task Create(Document p)
        {
            
            await documents.InsertOneAsync(p);
        }

        // удаление документа
        public async Task Remove(string id)
        {
            await documents.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}
