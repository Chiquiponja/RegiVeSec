using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using RegiVeSec.Data;
using RegiVeSec.Models;

namespace RegiVeSec.Controllers
{
    public class SesionController : Controller
    {
        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();
        public SesionController(Conexionbd _db)
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

        [HttpPost]
        public IActionResult LogIn(Login admin)
        {
            var _admin = db.Logins.Where(s => s.Nombre == admin.Nombre);
            if (_admin.Any())
            {
                if (_admin.Where(s => s.Contrasenia == admin.Contrasenia).Any())
                {

                    return Redirect("/Sesion/ContraseñaCorrecta");
                }
                else
                {
                    return Redirect("/Sesion/ContraseñaIncorrecta");
                }
            }
            else
            {
                return Redirect("/Sesion/ContraseñaIncorrecta");
            }
        }
        public Login GetLoginId(int id)
        {

            var Login = db.Logins.FirstOrDefault(x => x.Id == id);
            return Login;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }
        public IActionResult ContraseñaCorrecta(int id)
        {
            ViewData["Id"] = id;
            return View(GetLoginId(id));
        }
        public IActionResult ContraseñaIncorrecta()
        {
            return View();
        }
    }
}