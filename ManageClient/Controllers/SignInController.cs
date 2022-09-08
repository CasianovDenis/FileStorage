using ManageClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Controllers
{
    public class SignInController : Controller
    {
        private IHttpContextAccessor Accessor;
        private readonly ConString _conString;

        public SignInController(ConString conection, IHttpContextAccessor _accessor)
        {
            _conString = conection;
            this.Accessor = _accessor;
        }

        public IActionResult SignIn()
        {

            ViewBag.status_remember = this.Accessor.HttpContext.Request.Cookies["remember"];
            string status_account = this.Accessor.HttpContext.Request.Cookies["status_account"];

            if (status_account == "online") return RedirectToAction("Account", "Account");


            ViewBag.hide_layout = "true";
            ViewBag.hide_footer = "true";
           
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(Users_ManageProject user)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);


            try
            {
                var dbdata = _conString.Users_ManageProject.Single(data => data.Username == user.Username);
                ViewBag.dbdata = dbdata;
            }
            catch {
               ViewData["Error_SignIn"] = "Username or password is incorect";
                SignIn();
                return View(); 
            }
            
            if (ViewBag.dbdata != null)
            {
                var dbdata = _conString.Users_ManageProject.Single(data => data.Username == user.Username);
                
                string myPasswordUnencoded = DecodeFrom64(dbdata.Password);

                

                if (dbdata.Username == user.Username)
                {
                    if (myPasswordUnencoded == user.Password)
                    {
                        Response.Cookies.Append("username", user.Username, option);
                        Response.Cookies.Append("status_account", "online", option);
                        return RedirectToAction("Account", "Account");
                    }
                }
                ViewData["Error_SignIn"] = "Username or password is incorect";
                SignIn();
                return View();
               

            }           
                return View();
            
        }

        [HttpPost]
        public IActionResult Remember(string status)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("remember", status, option);

            return Ok();
        }

        static public string DecodeFrom64(string encodedData)

        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }
    }
}
