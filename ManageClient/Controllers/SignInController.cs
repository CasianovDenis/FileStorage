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
       
        private readonly ConString _conString;

        public SignInController(ConString conection)
        {
            _conString = conection;
           
        }

        public IActionResult SignIn()
        {
            ViewBag.hide_layout = "true";
            ViewBag.hide_footer = "true";
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(UserData user)
        {

            try
            {
                var dbdata = _conString.UserData.Single(data => data.Username == user.Username);
                ViewBag.dbdata = dbdata;
            }
            catch {
               ViewData["Error_SignIn"] = "Username or password is incorect";
                SignIn();
                return View(); 
            }
            
            if (ViewBag.dbdata != null)
            {
                var dbdata = _conString.UserData.Single(data => data.Username == user.Username);

                if (dbdata.Username == user.Username)
                {
                    if (dbdata.Password == user.Password)
                    {
                        return RedirectToAction("Account", "Account");
                    }
                }
                ViewData["Error_SignIn"] = "Username or password is incorect";
                SignIn();
                return View();
               

            }           
                return View();
            
        }
    }
}
