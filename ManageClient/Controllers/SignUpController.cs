using FluentValidation.AspNetCore;
using FluentValidation.Results;
using ManageClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ConString _conString;

        public SignUpController(ConString conection)
        {
            _conString = conection;

        }

        public IActionResult SignUp()
        {
            ViewBag.hide_layout = "true";
            ViewBag.hide_footer = "true";
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserData user)
        {

            ValidationUserData validationRules = new ValidationUserData(user);

            ValidationResult result = validationRules.Validate(user);



            if (result.IsValid == false)
            {
                
                result.AddToModelState(this.ModelState, "error posible");
                SignUp();
                return View();
            }

            //if (user.Username==""||user.Email==""||user.Password=="")
            //{
            //    ViewData["Error_SignUp"] = "";
            //    SignUp();
            //}
            try
            {
                var dbdata = _conString.UserData.Single(data => data.Username == user.Username);
                
            }
            catch
            {
                try
                {
                    var dbdata = _conString.UserData.Single(data => data.Email == user.Email);
                }
                catch
                {
                    UserServices userServices = new UserServices(_conString);
                    userServices.Create(user);
                    ViewData["Succes_create"] = "User was created succesfully";
                    SignUp();
                    return View();
                }
                
            }

            ViewData["Error_SignUp"] = "This user already exist";
            SignUp();
            return View();

        }
    }
}
