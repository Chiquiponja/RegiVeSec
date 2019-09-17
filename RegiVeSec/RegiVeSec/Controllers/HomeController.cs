using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using RegiVeSec.Models;
using Rotativa;

namespace RegiVeSec.Controllers
{
    public class HomeController : Controller
    {
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Vehiculos()
        {     
            return View();
        }
        public IActionResult PDF()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
