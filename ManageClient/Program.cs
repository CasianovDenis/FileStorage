using WebApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            // var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.Configure<MongoDBSettings>(
            //    builder.Configuration.GetSection("MongoDB"));

            //MongoClient client = new MongoClient("ATLAS_URI_HERE");

            //var playlistCollection = client.GetDatabase("sample_mflix").GetCollection<Playlist>("playlist");

            //List<string> movieList = new List<string>();
            //movieList.Add("1234");

            //playlistCollection.InsertOne(new Playlist("nraboy", movieList));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
       
    }
}
