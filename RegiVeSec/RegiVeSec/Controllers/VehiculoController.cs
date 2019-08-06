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
        nuevoVehiculo.FechaDeEntrega = Convert.ToDateTime( vehiculoRegiVeSecDto.FechaDeEntrega);
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
            var existentes = db.Tipos.ToList();
            var nuevos = new List<Tipo>(){        new Tipo{Detalles ="Moto"},        new Tipo{Detalles="Auto"},        new Tipo{Detalles = "Camion"},        new Tipo{Detalles="Camioneta"}      };

            foreach (var item in nuevos.Where(x => existentes.All(f => f.Detalles != x.Detalles)))
            {
                db.Tipos.Add(item);
                db.SaveChangesAsync();
            }

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

        [HttpGet]
    [Route("/Vehiculo/Buscar/{filtro}")]
    public List<VehiculoRegiVeSecDto> Buscar(string filtro)
    {

      var filtros = filtro.Split("|");

      List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

      foreach (var item in db.Vehiculos.Where(x=>x.FechaDeIngreso <= Convert.ToDateTime(filtros[1])
      && x.FechaDeIngreso>=Convert.ToDateTime(filtros[0])))
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
        public List<VehiculoRegiVeSecDto> Tabla()
        {

            string searchValue = Request.Form["search[value]"];


            var VehiculoRegiVeSecsPrueba = JsonConvert.DeserializeObject<List<VehiculoRegiVeSecDto>>(HttpContext.Session.GetString("Datos"));


            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                VehiculoRegiVeSecsPrueba = VehiculoRegiVeSecsPrueba.
                    Where(x => x.Marca.ToLower().Contains(searchValue.ToLower())).ToList<VehiculoRegiVeSecDto>();


            }
      var Listar = VehiculoRegiVeSecsPrueba;
            return Listar;

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
