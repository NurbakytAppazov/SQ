using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqApp.DbData;
using SqApp.Models;

namespace SqApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        public HomeController(AppDbContext _db)
        {
            this.db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Logistika()
        {
            return View();
        }

        public IActionResult Uslugi()
        {
            return View();
        }

        public IActionResult Dostavka()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult GetRequest([FromBody]RequestModel model)
        {
            if (model.PhoneNumber.Length != 0)
            {
                Request req = new Request { Name = model.Name, PhoneNumber = model.PhoneNumber, Description = model.Description, DateTime = DateTime.Now };
                db.Requests.Add(req);
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
