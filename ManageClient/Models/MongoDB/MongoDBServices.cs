﻿using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MongoDBServices 
    {
        

        //IGridFSBucket gridFS;   // файловое хранилище
        IMongoCollection<Playlist> playlist; // коллекция в базе данных
        public MongoDBServices()
        {
            // строка подключения
            string connectionString = "mongodb://localhost:27017/ManageClient";
            //string connectionString = "mongodb+srv://AngelBenny:alibaba321@manageproject.xl5mycl.mongodb.net/?retryWrites=true&w=majority";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            // получаем доступ к файловому хранилищу
            // gridFS = new GridFSBucket(database);
            // обращаемся к коллекции Products
            playlist = database.GetCollection<Playlist>("collectiondata");
            //playlist = database.GetCollection<Playlist>("mytable");
        }
        // получаем все документы, используя критерии фальтрации
        public async Task<IEnumerable<Playlist>> GetProducts(string name, string surname)
        {
            // строитель фильтров
            var builder = new FilterDefinitionBuilder<Playlist>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            // фильтр по имени
            if (!String.IsNullOrWhiteSpace(name))
            {
                filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
            }
            //if (minPrice.HasValue)  // фильтр по минимальной цене
            //{
            //    filter = filter & builder.Gte("Price", minPrice.Value);
            //}
            //if (maxPrice.HasValue)  // фильтр по максимальной цене
            //{
            //    filter = filter & builder.Lte("Price", maxPrice.Value);
            //}

            return await playlist.Find(filter).ToListAsync();
        }

        // получаем один документ по id
        public async Task<Playlist> GetProduct(string id)
        {
            return await playlist.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        // добавление документа
        //public async Task Create(Product p)
        //{
        //    await Products.InsertOneAsync(p);
        //}
        // обновление документа
        //public async Task Update(Product p)
        //{
        //    await Products.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.Id)), p);
        //}
        // удаление документа
        // public async Task Remove(string id)
        //{
        //    await Products.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        //}
        // получение изображения
        //public async Task<byte[]> GetImage(string id)
        //{
        //    return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        //}
        // сохранение изображения
        //public async Task StoreImage(string id, Stream imageStream, string imageName)
        //{
        //    Product p = await GetProduct(id);
        //    if (p.HasImage())
        //    {
        //        // если ранее уже была прикреплена картинка, удаляем ее
        //        await gridFS.DeleteAsync(new ObjectId(p.ImageId));
        //    }
        //    // сохраняем изображение
        //    ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);
        //    // обновляем данные по документу
        //    p.ImageId = imageId.ToString();
        //    var filter = Builders<Product>.Filter.Eq("_id", new ObjectId(p.Id));
        //    var update = Builders<Product>.Update.Set("ImageId", p.ImageId);
        //    await Products.UpdateOneAsync(filter, update);
        //}
    }
}
