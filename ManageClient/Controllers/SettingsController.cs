using ManageClient.Models;
using ManageClient.Models.MSSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace ManageClient.Controllers
{
    public class SettingsController : Controller
    {
        private IHttpContextAccessor Accessor;
        private readonly ConString _conString;


        public SettingsController(ConString conection, IHttpContextAccessor _accessor)
        {

            _conString = conection;
            this.Accessor = _accessor;


        }

        public IActionResult Settings()
        {
            string status_account = this.Accessor.HttpContext.Request.Cookies["status_account"];
            string username = this.Accessor.HttpContext.Request.Cookies["username"];

            if (status_account == "offline") RedirectToAction("Index", "Home");

            ViewBag.Name = username;

            var dbdata = _conString.Users_ManageProject.Single(data => data.Username == username);


            string hide_pass = "";

            for (int index = 0; index < dbdata.Password.Length; index++)
            {
                hide_pass = hide_pass + "*";
            }

            dbdata.Password = hide_pass;
            ViewBag.dbdata = dbdata;

            return View();
        }

        [HttpPost]
        public IActionResult update_username(TempData temp)
        {
            try
            {
                var new_username = _conString.Users_ManageProject.Single(data => data.Username == temp.NewUsername);
            }
            catch
            {
                string username = this.Accessor.HttpContext.Request.Cookies["username"];

               
                
                var dbdata = _conString.Users_ManageProject.Single(data => data.Username == username);
                dbdata.Username = temp.NewUsername;

                
                _conString.SaveChanges();


                MongoDBServices dBServices = new MongoDBServices();
                var allfile = dBServices.GetAll(username);
                

                foreach (var item in allfile)
                {
                    Document docs = new Document();

                    docs._id = item._id;
                    docs.Username = temp.NewUsername;
                    docs.ImageName = item.ImageName;
                    docs.ImageUrl = item.ImageUrl;

                    dBServices.Update(docs);
                }

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);

                Response.Cookies.Append("username", temp.NewUsername, option);

                 return RedirectToAction("Settings", "Settings");
            }

            ViewData["Error_update_username"] = "This username already exist";
            Settings();
            return View("Settings");
            
        }

        [HttpPost]
        public IActionResult update_email(TempData temp)
        {
            try
            {
                var new_username = _conString.Users_ManageProject.Single(data => data.Email == temp.NewEmail);
            }
            catch
            {
                string username = this.Accessor.HttpContext.Request.Cookies["username"];

                

                var dbdata = _conString.Users_ManageProject.Single(data => data.Username == username);
                dbdata.Email = temp.NewEmail;


                _conString.SaveChanges();

                

                return RedirectToAction("Settings", "Settings");
            }

            ViewData["Error_update_email"] = "This email already exist";
            Settings();
            return View("Settings");
            
        }

        [HttpPost]
        public IActionResult update_password(TempData temp)
        {
            string username = this.Accessor.HttpContext.Request.Cookies["username"];

            var dbdata = _conString.Users_ManageProject.Single(data => data.Username == username);

            
            string decodedpassword = DecodeFrom64(dbdata.Password);

            


            if (temp.OldPassword == decodedpassword)
            {
                if (temp.NewPassword != temp.OldPassword)
                {
                    if (temp.NewPassword == temp.Verification_pass)
                    {

                        string PasswordEncoded = EncodeTo64(temp.NewPassword);

                        dbdata.Password = PasswordEncoded;


                        _conString.SaveChanges();



                        return RedirectToAction("Settings", "Settings");
                    }
                    else
                    {
                        ViewData["Error_update_password"] = "New Password not match verification password";
                        Settings();
                        return View("Settings");
                    }
                }
                else
                {

                    ViewData["Error_update_password"] = "New password match old";
                    Settings();
                    return View("Settings");
                }
            }
            else
            {
                ViewData["Error_update_password"] = "Current Password not match";
                Settings();
                return View("Settings");
            }

        }

        static public string DecodeFrom64(string encodedData)

        {

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);

            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }

        static public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
    }
}
