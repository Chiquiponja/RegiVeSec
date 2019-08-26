using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using RegiVeSec.Data;
using RegiVeSec.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RegiVeSec.Controllers
{
    public class SesionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();
        public SesionController(Conexionbd _db)
        {
          db = _db;
        }
    public List<Login> Listar()
    {

      List<Login> PersonasPrueba = new List<Login>();

      //foreach (var item in db.Logins.ToList())
      //{
        //VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

        //  dto.Id = item.Id;
        //  dto.FechaDeIngreso = item.FechaDeIngreso.ToShortDateString();
        //  dto.Propietario = item.Propietario;
        //  dto.Dominio = item.Dominio;
        //  dto.DetallesVehiculo = "Dominio: (" + item.Dominio + ") Tipo: (" + item.Tipo + ") Marca: (" + item.Marca + ") Color: (" + item.Color + ") Modelo: (" + item.Modelo + ") Estado: (" + item.Estado + ") ";
        //  dto.Tipo = item.Tipo;
        //  dto.Marca = item.Marca;
        //  dto.Color = item.Color;
        //  dto.Modelo = item.Modelo;
        //  dto.Causa = item.Causa;
        //  dto.Estado = item.Estado;
        //  dto.NumeroSumario = item.NumeroSumario;
        //  dto.Dependencia = item.Dependencia;
        //  dto.Orden = item.Orden;
        //  dto.DependenciaProcedente = item.DependenciaProcedente;
        //  dto.Observaciones = item.Observaciones;
        //  dto.Recibe = item.Recibe;
        //  dto.Entrega = item.Entrega;
        //  dto.FechaDeEntrega = item.FechaDeEntrega.ToShortDateString();


        //PersonasPrueba.Add(PersonasPrueba);
        //}




        HttpContext.Session.SetString("Datos", JsonConvert.SerializeObject(PersonasPrueba));

      return PersonasPrueba;
    }



  }
}
