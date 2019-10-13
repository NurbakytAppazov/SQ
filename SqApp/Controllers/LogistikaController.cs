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

        public IActionResult Air()
        {
            return View();
        }
        public IActionResult Avto()
        {
            return View();
        }

        public IActionResult Cheap()
        {
            return View();
        }
        public IActionResult More()
        {
            return View();
        }
        public IActionResult Rail()
        {
            return View();
        }
    }
}