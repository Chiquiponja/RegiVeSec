using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegiVeSec.Data;
using RegiVeSec.Models;

namespace RegiVeSec.Controllers
{
    public class SeccionController : Controller
    {
        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();
        public SeccionController(Conexionbd _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Add(Login en)
        {
            try
            {
                //throw new Exception("No se pudo guardar el vehiculo.");


                db.Logins.Add(en);
                await db.SaveChangesAsync();

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }

        }
        public IActionResult Index()
        {
            return View();
        }

    }
}