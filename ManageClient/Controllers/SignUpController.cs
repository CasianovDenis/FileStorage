using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
