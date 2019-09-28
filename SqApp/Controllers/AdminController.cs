using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SqApp.Controllers
{
    [Authorize(Roles = "Oppo")]
    public class AdminController : Controller
    {



        public IActionResult Admin()
        {
            return View();
        }
    }
}