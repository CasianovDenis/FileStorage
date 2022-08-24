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

        //public async Task<IActionResult> Index(Playlist playlist)
        //{
        //    var phones = await _mongoDBService.GetProducts(playlist.name,playlist.surname);
        //    //var model = new IndexViewModel { Products = phones, Filter = filter };
        //    ViewBag.test = phones;

        //    ViewBag.hide_layout = "true";
        //    return View();
        //}

        public IActionResult Index()
        {
            ViewBag.hide_layout = "true";
            return View();
        }

        [HttpPost]
        public IActionResult Redirect_SignIn()
        {
            return RedirectToAction("SignIn","SignIn");
        }

        [HttpPost]
        public IActionResult Redirect_SignUp()
        {
            return RedirectToAction("SignUp", "SignUp");
        }

        
    }
}
