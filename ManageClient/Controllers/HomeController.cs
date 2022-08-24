using WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MongoDBServices _mongoDBService;
        

        public HomeController(ILogger<HomeController> logger , MongoDBServices context)
        {
            _logger = logger;
            _mongoDBService = context;
        }

        public async Task<IActionResult> Index(Playlist playlist)
        {
            var phones = await _mongoDBService.GetProducts(playlist.name,playlist.surname);
            //var model = new IndexViewModel { Products = phones, Filter = filter };
            ViewBag.test = phones;

            ViewBag.hide_layout = "true";
            return View();
        }

        //[HttpGet]
        //public async Task<List<Playlist>> Get() {
        //    return _mongoDBService.Get();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Playlist playlist) { }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) { }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string i


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
