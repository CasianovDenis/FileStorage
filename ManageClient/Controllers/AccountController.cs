using ManageClient.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using WebApplication.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ManageClient.Controllers
{
    public class AccountController : Controller
    {
        private IHttpContextAccessor Accessor;
        private readonly ConString _conString;
        private IWebHostEnvironment _appEnvironment;
       // private readonly MongoDBServices _mongoDBService;
        public AccountController(ConString conection, IHttpContextAccessor _accessor, IWebHostEnvironment appEnvironment)
        {
            _conString = conection;
            this.Accessor = _accessor;
            _appEnvironment = appEnvironment;
           // _mongoDBService = context;
        }

        public IActionResult Account()
        {
            string status_account = this.Accessor.HttpContext.Request.Cookies["status_account"];
            string username = this.Accessor.HttpContext.Request.Cookies["username"];
            ViewBag.Name = username;
            if (status_account == "offline") return RedirectToAction("Index", "Home");


            MongoDBServices dBServices = new MongoDBServices();
            var userdata = dBServices.GetAll(username);

            //ViewBag.test = userdata.ImageId;
            //ViewBag.GetAll = userdata;
            List<string> type_img = new List<string>();
            List<string> name_img = new List<string>();

            List<string> type_doc = new List<string>();
            
            List<string> type_audio = new List<string>();
            List<string> name_audio = new List<string>();

            List<string> type_video = new List<string>();
            List<string> name_video = new List<string>();

            foreach (var item in userdata)
            {
                string type_file = "";

                for (int index = item.ImageName.Length - 3; index < item.ImageName.Length; index++)
                {
                    type_file = type_file + item.ImageName[index];
                }

                if (type_file == "png" || type_file == "jpg")
                {
                   
                    name_img.Add(item.ImageName);
                    type_img.Add(item.ImageUrl);
                }

                else
                    if (type_file == "pdf" || type_file == "txt" || type_file == "docx") type_doc.Add(item.ImageUrl);
                else
                    if (type_file == "mp3")
                {
                    
                   
                    type_audio.Add(item.ImageUrl);
                    name_audio.Add(item.ImageName);
                }
                else
                    if (type_file == "mp4" || type_file == "avi" || type_file == "mpg" || type_file == "ogg" || type_file == "mov")
                {
                    name_video.Add(item.ImageName);
                    type_video.Add(item.ImageUrl); 
                }

            }


            ViewBag.doc = type_doc;

            ViewBag.img = type_img;
            ViewBag.name_img = name_img;

            ViewBag.video = type_video;
            ViewBag.name_video = name_video;

            ViewBag.audio = type_audio;
            ViewBag.name_audio = name_audio;

            return View();
        }

       


        [HttpPost]
        public ActionResult AddFile(FileModel uploadfile)
        {
            string username = this.Accessor.HttpContext.Request.Cookies["username"];
            MongoDBServices dBServices = new MongoDBServices();
            
            Document playlist = new Document();
            playlist.Username = username;
            playlist.ImageName = uploadfile.file.FileName;
            if (uploadfile.file.ContentDisposition != null)
            {
                MemoryStream memdata = new MemoryStream();
                uploadfile.file.CopyTo(memdata);
                playlist.ImageUrl = Convert.ToBase64String(memdata.ToArray());

                var data = dBServices.Create(playlist);
              
                }

            return RedirectToAction("Account", "Account");
        }

        
        public ActionResult Exit()
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("username", "", option);
            Response.Cookies.Append("status_account", "offline", option);

            return RedirectToAction("SignIn", "SignIn");
        }
    }
}
