using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegiVeSec.Data;
using RegiVeSec.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegiVeSec.Controllers
{
    public class VehiculoController : Controller
    {
        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();
        public VehiculoController(Conexionbd _db)
        {
            db = _db;
        }

        [Authorize]
        public IActionResult Eliminar(int id)
        {
            ViewData["Id"] = id;
            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);

            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El Vehiculo con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
        }
        [Authorize]
        public IActionResult Detalles(int id)
        {
            ViewData["Id"] = id;
            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);

            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El Vehiculo con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
        }
        [Authorize]
        public IActionResult Agregar(int id)
        {
            ViewData["Id"] = id;
            return View(GetVehiculoRegiVeSecId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]VehiculoRegiVeSecDto vehiculoRegiVeSecDto)
        {
            var nuevoVehiculo = new VehiculoRegiVeSec();
            nuevoVehiculo.Color = vehiculoRegiVeSecDto.Color;

            nuevoVehiculo.Causa = vehiculoRegiVeSecDto.Causa;
            nuevoVehiculo.Dependencia = vehiculoRegiVeSecDto.Dependencia;
            nuevoVehiculo.DependenciaProcedente = vehiculoRegiVeSecDto.DependenciaProcedente;
            nuevoVehiculo.Dominio = vehiculoRegiVeSecDto.Dominio;
            nuevoVehiculo.Entrega = vehiculoRegiVeSecDto.Entrega;
            nuevoVehiculo.Estado = vehiculoRegiVeSecDto.Estado;
            nuevoVehiculo.FechaDeEntrega = Convert.ToDateTime(vehiculoRegiVeSecDto.FechaDeEntrega);
            nuevoVehiculo.FechaDeIngreso = Convert.ToDateTime(vehiculoRegiVeSecDto.FechaDeIngreso);
            nuevoVehiculo.Id = vehiculoRegiVeSecDto.Id;
            nuevoVehiculo.Marca = vehiculoRegiVeSecDto.Marca;
            nuevoVehiculo.Modelo = vehiculoRegiVeSecDto.Modelo;
            nuevoVehiculo.NumeroSumario = vehiculoRegiVeSecDto.NumeroSumario;
            nuevoVehiculo.Observaciones = vehiculoRegiVeSecDto.Observaciones;
            nuevoVehiculo.Orden = vehiculoRegiVeSecDto.Orden;
            nuevoVehiculo.Propietario = vehiculoRegiVeSecDto.Propietario;
            nuevoVehiculo.Recibe = vehiculoRegiVeSecDto.Recibe;

            nuevoVehiculo.Tipo = db.Tipos.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Tipo.Id);

            try
            {

                //throw new Exception("No se pudo guardar el vehiculo.");


                db.Vehiculos.Add(nuevoVehiculo);
                await db.SaveChangesAsync();

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }

        }
        [Route("/Vehiculo/Inicializador")]

        public void Inicializador()
        {
            InicializerTipos();
        }

        private void InicializerTipos()
        {
            var iniTipos = new InicializacionTipo(db);
            iniTipos.IniTipos();

        }

        public List<VehiculoRegiVeSecDto> Listar()
        {

            List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

            foreach (var item in db.Vehiculos.ToList())
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

                dto.Id = item.Id;
                dto.FechaDeIngreso = item.FechaDeIngreso.ToShortDateString();
                dto.Propietario = item.Propietario;
                dto.Dominio = item.Dominio;
                dto.DetallesVehiculo = "Dominio: (" + item.Dominio + ") Tipo: (" + item.Tipo + ") Marca: (" + item.Marca + ") Color: (" + item.Color + ") Modelo: (" + item.Modelo + ") Estado: (" + item.Estado + ") ";
                dto.Tipo = item.Tipo;
                dto.Marca = item.Marca;
                dto.Color = item.Color;
                dto.Modelo = item.Modelo;
                dto.Causa = item.Causa;
                dto.Estado = item.Estado;
                dto.NumeroSumario = item.NumeroSumario;
                dto.Dependencia = item.Dependencia;
                dto.Orden = item.Orden;
                dto.DependenciaProcedente = item.DependenciaProcedente;
                dto.Observaciones = item.Observaciones;
                dto.Recibe = item.Recibe;
                dto.Entrega = item.Entrega;
                dto.FechaDeEntrega = item.FechaDeEntrega.ToShortDateString();


                VehiculoRegiVeSecsPrueba.Add(dto);
            }



            
        HttpContext.Session.SetString("Datos", JsonConvert.SerializeObject(VehiculoRegiVeSecsPrueba));

            return VehiculoRegiVeSecsPrueba;
        }
        //public IEnumerable<VehiculoRegiVeSecDto> ListStores(Expression<Func<VehiculoRegiVeSecDto, string>> sort, bool desc, int page, int pageSize, out int totalRecords)
        //{
        //    List<VehiculoRegiVeSecDto> stores = new List<VehiculoRegiVeSecDto>();
        //    using (var context = new TectonicEntities())
        //    {
        //        totalRecords = context.Stores.Count();
        //        int skipRows = (page - 1) * pageSize;
        //        if (desc)
        //            stores = context.Stores.OrderByDescending(sort).Skip(skipRows).Take(pageSize).ToList();
        //        else
        //            stores = context.Stores.OrderBy(sort).Skip(skipRows).Take(pageSize).ToList();
        //    }
        //    return stores;
        //}
        [HttpGet]
        [Route("/Vehiculo/Buscar/{filtro}")]
        public List<VehiculoRegiVeSecDto> Buscar(string filtro)
        {

            var filtros = filtro.Split("|");

            List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

            var textoABuscar = filtros[0];

            var filtraTexto = !string.IsNullOrWhiteSpace(textoABuscar);

            var fechaHasta = new DateTime();
            var fechaDesde = new DateTime();
            var filtraFecha = DateTime.TryParse(filtros[1], out fechaDesde) && DateTime.TryParse(filtros[2], out fechaHasta);

            foreach (var item in db.Vehiculos.Where(x =>
            ( !filtraTexto || (x.Dominio.ToLower().Contains(textoABuscar) ||
            x.Propietario.ToLower().Contains(textoABuscar))) &&
            (!filtraFecha || (x.FechaDeIngreso >= fechaDesde
            && x.FechaDeIngreso <= fechaHasta))))
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

                dto.Id = item.Id;
                dto.FechaDeIngreso = item.FechaDeIngreso.ToShortDateString();
                dto.Propietario = item.Propietario;
                dto.Dominio = item.Dominio;
                dto.DetallesVehiculo = "Dominio: (" + item.Dominio + ") Tipo: (" + item.Tipo + ") Marca: (" + item.Marca + ") Color: (" + item.Color + ") Modelo: (" + item.Modelo + ") Estado: (" + item.Estado + ") ";
                dto.Tipo = item.Tipo;
                dto.Marca = item.Marca;
                dto.Color = item.Color;
                dto.Modelo = item.Modelo;
                dto.Causa = item.Causa;
                dto.Estado = item.Estado;
                dto.NumeroSumario = item.NumeroSumario;
                dto.Dependencia = item.Dependencia;
                dto.Orden = item.Orden;
                dto.DependenciaProcedente = item.DependenciaProcedente;
                dto.Observaciones = item.Observaciones;
                dto.Recibe = item.Recibe;
                dto.Entrega = item.Entrega;
                dto.FechaDeEntrega = item.FechaDeEntrega.ToShortDateString();


                VehiculoRegiVeSecsPrueba.Add(dto);
            }
            return VehiculoRegiVeSecsPrueba;
        }
        [HttpPost]
        [Route("/Vehiculo/Tabla")]
        public ActionResult<List<VehiculoRegiVeSec>> Tabla()
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            List<VehiculoRegiVeSec> empList = new List<VehiculoRegiVeSec>();

            empList = db.Vehiculos.ToList();
            int totalrows = empList.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                empList = empList.
                    Where(x => x.Marca.ToLower().Contains(searchValue.ToLower()) || x.Dependencia.ToLower().Contains(searchValue.ToLower()) || x.Propietario.ToLower().Contains(searchValue.ToLower()) || x.Causa.ToString().Contains(searchValue.ToLower()) || x.Color.ToString().Contains(searchValue.ToLower())).ToList<VehiculoRegiVeSec>();
            }
            int totalrowsafterfiltering = empList.Count;

            empList = empList.Skip(start).Take(length).ToList<VehiculoRegiVeSec>();

            return new JsonResult(empList);
            //}

            //      string searchValue = Request.Form["search[value]"];


            //      var VehiculoRegiVeSecsPrueba = JsonConvert.DeserializeObject<List<VehiculoRegiVeSecDto>>(HttpContext.Session.GetString("Datos"));


            //      if (!string.IsNullOrEmpty(searchValue))//filter
            //      {
            //          VehiculoRegiVeSecsPrueba = VehiculoRegiVeSecsPrueba.
            //              Where(x => x.Marca.ToLower().Contains(searchValue.ToLower())).ToList<VehiculoRegiVeSecDto>();
            //      }
            //var Listar = VehiculoRegiVeSecsPrueba;
            //      return Listar;

        }

        public async Task<IActionResult> Delete(VehiculoRegiVeSec en)
        {

            try
            {
                //throw new Exception("No se pudo Eliminar el Registro.");
                en = GetVehiculoRegiVeSecId(en.Id);


                db.Remove(en);
                await db.SaveChangesAsync();

                return Redirect("/Home/Index/");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }


        }

        public VehiculoRegiVeSec GetVehiculoRegiVeSecId(int id)
        {

            var VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Tipo).FirstOrDefault(x => x.Id == id);
            //VehiculoRegiVeSec.Tipo = ;

            return VehiculoRegiVeSec;
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]VehiculoRegiVeSecDto vehiculoRegiVeSecDto)
        {
            var nuevoVehiculo = new VehiculoRegiVeSec();
            nuevoVehiculo.Color = vehiculoRegiVeSecDto.Color;
            nuevoVehiculo.Causa = vehiculoRegiVeSecDto.Causa;
            nuevoVehiculo.Dependencia = vehiculoRegiVeSecDto.Dependencia;
            nuevoVehiculo.DependenciaProcedente = vehiculoRegiVeSecDto.DependenciaProcedente;
            nuevoVehiculo.Dominio = vehiculoRegiVeSecDto.Dominio;
            nuevoVehiculo.Entrega = vehiculoRegiVeSecDto.Entrega;
            nuevoVehiculo.Estado = vehiculoRegiVeSecDto.Estado;
            nuevoVehiculo.FechaDeEntrega = Convert.ToDateTime(vehiculoRegiVeSecDto.FechaDeEntrega);
            nuevoVehiculo.FechaDeIngreso = Convert.ToDateTime(vehiculoRegiVeSecDto.FechaDeIngreso);
            nuevoVehiculo.Id = vehiculoRegiVeSecDto.Id;
            nuevoVehiculo.Marca = vehiculoRegiVeSecDto.Marca;
            nuevoVehiculo.Modelo = vehiculoRegiVeSecDto.Modelo;
            nuevoVehiculo.NumeroSumario = vehiculoRegiVeSecDto.NumeroSumario;
            nuevoVehiculo.Observaciones = vehiculoRegiVeSecDto.Observaciones;
            nuevoVehiculo.Orden = vehiculoRegiVeSecDto.Orden;
            nuevoVehiculo.Propietario = vehiculoRegiVeSecDto.Propietario;
            nuevoVehiculo.Recibe = vehiculoRegiVeSecDto.Recibe;

            nuevoVehiculo.Tipo = db.Tipos.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Tipo.Id);
            try
            {
                //throw new Exception("No se pudo Editar el Registro.");
                db.Vehiculos.Update(nuevoVehiculo);
                await db.SaveChangesAsync();
                return Redirect("/Home/Index/");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }

        }
        [Authorize]
        public IActionResult Editar(int id)
        {
            ViewData["Id"] = id;

            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);
            VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Tipo).FirstOrDefault(x => x.Id == id);
            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El VehiculoRegiVeSec con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
        }
        public IEnumerable<VehiculoRegiVeSec> Lista()
        {
            var Vehiculos = db.Vehiculos.Include("Tipo").ToList();

            return Vehiculos;
        }
        //}

        [HttpGet]
        [Route("/Vehiculo/GetTipos")]
        public List<Tipo> GetTipos()
        {
            var result = db.Tipos;
            return result.ToList();
            //    return db.Contactos;
        }



        public List<Tipo> ObtenerTodos()
        {
            using (var db = new Conexionbd())
            {
                return db.Tipos.ToList();
            }
        }
    }
}
