using FluentValidation.AspNetCore;
using FluentValidation.Results;
using ManageClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Controllers
{
    public class SignUpController : Controller
    {
        private IHttpContextAccessor Accessor;
        private readonly ConString _conString;

        public SignUpController(ConString conection, IHttpContextAccessor _accessor)
        {
            _conString = conection;
            this.Accessor = _accessor;
        }

        public IActionResult SignUp()
        {

            string status_account = this.Accessor.HttpContext.Request.Cookies["status_account"];

            if (status_account == "online") return RedirectToAction("Account", "Account");
            

            ViewBag.hide_layout = "true";
            ViewBag.hide_footer = "true";
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Users_ManageProject user)
        {

            ValidationUserData validationRules = new ValidationUserData(user);

            ValidationResult result = validationRules.Validate(user);



            if (result.IsValid == false)
            {
                
                result.AddToModelState(this.ModelState, "error posible");
                SignUp();
                return View();
            }

            
            try
            {
                var dbdata = _conString.Users_ManageProject.Single(data => data.Username == user.Username);
                
            }
            catch
            {
                try
                {
                    var dbdata = _conString.Users_ManageProject.Single(data => data.Email == user.Email);
                }
                catch
                {
                    if (user.Password == user.Password_verification)
                    {
                        UserServices userServices = new UserServices(_conString);
                        user.Password_verification = null;

                        string myPasswordEncoded = EncodeTo64(user.Password);
                        user.Password=myPasswordEncoded;

                        userServices.Create(user);
                        ViewData["Succes_create"] = "User was created succesfully";
                    }
                    else
                        ViewData["Error_SignUp"] = "Password do not match";

                    //UserServices userServices = new UserServices(_conString);
                    //userServices.Create(user);
                    //ViewData["Succes_create"] = "User was created succesfully";
                    SignUp();
                    return View();
                }
                
            }

            ViewData["Error_SignUp"] = "This user already exist";
            SignUp();
            return View();

        }

        static public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
    }
}
