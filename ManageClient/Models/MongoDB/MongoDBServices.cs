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
        IMongoCollection<Document> playlist; // коллекция в базе данных

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
            gridFS = new GridFSBucket(database);

            // обращаемся к коллекции Products
            playlist = database.GetCollection<Document>("collectiondata");
            //playlist = database.GetCollection<Playlist>("mytable");
        }
        // получаем все документы, используя критерии фальтрации
        public async Task<IEnumerable<Document>> GetProducts(string name, string surname)
        {
            // строитель фильтров
            var builder = new FilterDefinitionBuilder<Document>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            // фильтр по имени
            //if (!String.IsNullOrWhiteSpace(name))
            //{
            //    filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
            //}
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
        public async Task<Document> GetProduct(string id)
        {
            return await playlist.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        // получаем один документ по id
        public Document Get(string username)
        {
            return playlist.Find(new BsonDocument("Username", new string(username))).FirstOrDefault();
        }

        public IEnumerable<Document> GetAll(string username)
        {
            var get = playlist.Find(new BsonDocument("Username", new string(username))).ToList();
            return get;
        }
        // добавление документа
        public async Task Create(Document p)
        {

            await playlist.InsertOneAsync(p);
        }
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
        //public async Task<Stream> GetImageAsync(string id)
        //{
        //    //using (Stream fs = new FileStream("cat_new.jpg", FileMode.OpenOrCreate))
        //    //{
        //    //   await gridFS.DownloadToStreamAsync(new ObjectId(id), fs);
        //    //    return fs;
        //    //}

        //    //var thePictureColleciton = GetPictureCollection();

        //    ////get picture document from db
        //    //var thePicture = thePictureColleciton.FindOneById(new ObjectId(id));

        //    ////transform the picture's data from string to an array of bytes
        //    //var thePictureDataAsBytes = Convert.FromBase64String(thePicture.PictureDataAsString);

        //    ////return array of bytes as the image's data to action's response. 
        //    ////We set the image's content mime type to image/jpeg
        //    //return new FileContentResult(thePictureDataAsBytes, "image/jpeg");


        //    //return await gridFS.DownloadToStreamAsync(new ObjectId("630a098c91078de990de16d2"));
        //}
        // сохранение изображения
        public async Task StoreImage(MemoryStream imageStream, string imagename, string id)
        {
            // Document p = await GetProduct(id);

            //// сохраняем изображение
            // ObjectId imageId = await gridFS.UploadFromStreamAsync(imagename, imageStream);
            // обновляем данные по документу

            var filter = Builders<Document>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<Document>.Update.Set("ImageUrl", imageStream);
            await playlist.UpdateOneAsync(filter, update);
        }
    }
}