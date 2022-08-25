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
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor Accessor;
        private readonly ILogger<HomeController> _logger;
        private readonly MongoDBServices _mongoDBService;
        

        public HomeController(ILogger<HomeController> logger , MongoDBServices context, IHttpContextAccessor _accessor)
        {
            _logger = logger;
            _mongoDBService = context;
            this.Accessor = _accessor;
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
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);


            string status_remember = this.Accessor.HttpContext.Request.Cookies["remember"];
            string status_account = this.Accessor.HttpContext.Request.Cookies["status_account"];
            if (status_remember == null)
            {
                Response.Cookies.Append("remember", "unchecked", option);
                status_remember = "uncheked";
            }
            else
            if (status_account == null)
            {
                Response.Cookies.Append("status_account", "offline", option);
            }

            if (status_remember == "checked") { if (status_account == "online") return RedirectToAction("Account","Account"); }
            else
                if (status_remember == "unchecked") Response.Cookies.Append("status_account", "offline", option);

            
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
