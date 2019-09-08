using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SqApp.Controllers
{
    public class LogistikaController : Controller
    {
        public IActionResult Logistika()
        {
            return View();
        }
    }
}