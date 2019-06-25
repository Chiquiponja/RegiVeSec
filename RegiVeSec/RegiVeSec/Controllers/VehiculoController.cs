using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegiVeSec.Data;
using RegiVeSec.Models;

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


        public IActionResult Eliminar(int id)
        {
            ViewData["Id"] = id;
            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);

            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El VehiculoRegiVeSec con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
        }
        public IActionResult Agregar(int id)
        {
            ViewData["Id"] = id;
            return View(GetVehiculoRegiVeSecId(id));
        }


        public async Task<IActionResult> Add(VehiculoRegiVeSec en)
        {
            try
            {
                //throw new Exception("No se pudo guardar el vehiculo.");


                db.Vehiculos.Add(en);
                await db.SaveChangesAsync();

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }





        }

        public List<VehiculoRegiVeSecDto> Listar()
        {
            List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

            foreach (var item in db.Vehiculos.ToList())
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

                dto.Id = item.Id;
                dto.FechaDeIngreso = item.FechaDeIngreso;
                dto.Propietario = item.Propietario;
                dto.Dominio = item.Dominio;
                dto.DetallesVehiculo = "Dominio: ("+item.Dominio +") Tipo: (" + item.Tipo + ") Marca: (" +  item.Marca + ") Color: (" + item.Color+") Modelo: ("+ item.Modelo+") Estado: ("+ item.Estado + ") " ;
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
                dto.FechaDeEntrega = item.FechaDeEntrega;


        VehiculoRegiVeSecsPrueba.Add(dto);
            }
            return VehiculoRegiVeSecsPrueba;
        }
    public List<VehiculoRegiVeSecDto> Buscar(string filtro)
    {

      var filtros = filtro.Split("|");

      List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

      foreach (var item in db.Vehiculos.Where(x=>x.FechaDeEntrega<=Convert.ToDateTime(filtros[1])
      && x.FechaDeIngreso>=Convert.ToDateTime(filtros[0])))
      {
        VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

        dto.Id = item.Id;
        dto.FechaDeIngreso = item.FechaDeIngreso;
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
        dto.FechaDeEntrega = item.FechaDeEntrega;


        VehiculoRegiVeSecsPrueba.Add(dto);
      }
      return VehiculoRegiVeSecsPrueba;
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

            var VehiculoRegiVeSec = db.Vehiculos.FirstOrDefault(x => x.Id == id);
            return VehiculoRegiVeSec;
        }

        public async Task<IActionResult> Edit(VehiculoRegiVeSec en)
        {

            try
            {
                //throw new Exception("No se pudo Editar el Registro.");
                db.Vehiculos.Update(en);
                await db.SaveChangesAsync();
                return Redirect("/Home/Index/");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }


        }

        public IActionResult Editar(int id)
        {
            ViewData["Id"] = id;

            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);

            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El VehiculoRegiVeSec con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
        }

    }
}
