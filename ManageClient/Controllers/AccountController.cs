using ManageClient.Models;
using ManageClient.Models.List;
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
        
        public AccountController(ConString conection, IHttpContextAccessor _accessor, IWebHostEnvironment appEnvironment)
        {
            _conString = conection;
            this.Accessor = _accessor;
            _appEnvironment = appEnvironment;
            
        }

        public IActionResult Account()
        {
            string status_account = this.Accessor.HttpContext.Request.Cookies["status_account"];
            string username = this.Accessor.HttpContext.Request.Cookies["username"];
            string status_remember = this.Accessor.HttpContext.Request.Cookies["remember"];
            ViewBag.Name = username;
            if (status_account == "offline") return RedirectToAction("Index", "Home");

            //update cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("username", username, option);
            Response.Cookies.Append("status_account", "online", option);           
            Response.Cookies.Append("remember", status_remember, option);

            //Create page view
            MongoDBServices dBServices = new MongoDBServices();
            var userdata = dBServices.GetAll(username);


            List<Photo> photo = new List<Photo>();

            List<Docs> document = new List<Docs>();


            foreach (var item in userdata)
            {
                string type_file = "";

                for (int index = item.ImageName.Length - 4; index < item.ImageName.Length; index++)
                {
                    type_file = type_file + item.ImageName[index];
                }

                if (type_file.Trim('.') == "png" || type_file.Trim('.') == "jpg"|| type_file == "webp" || type_file == "jpeg")
                {

                    var data = new Photo() { _id=item._id,type_image = type_file.Trim('.'), name_image = item.ImageName, base64 = item.ImageUrl };
                    photo.Add(data);
                }
                else
                
                {
                    var data = new Docs() { _id = item._id,type_doc = type_file.Trim('.'), name_doc = item.ImageName, base64 = item.ImageUrl };
                    document.Add(data);
                }
            }

            
            ViewBag.photo = photo;

            ViewBag.doc_data = document;



            return View();
        }




        [HttpPost]
        public async Task<ActionResult> AddFileAsync(FileModel uploadfile)
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

                await dBServices.Create(playlist);

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

        [HttpPost]
        public ActionResult DeleteFile(string id)
        {

            MongoDBServices dBServices = new MongoDBServices();
            var remove = dBServices.Remove(id);
           
            return RedirectToAction("Account", "Account");
        }
    }
}
