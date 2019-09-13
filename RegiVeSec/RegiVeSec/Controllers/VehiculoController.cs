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
using RegiVeSec.Models.Dto;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Net.Http.Headers;
using System.IO;

namespace RegiVeSec.Controllers
{
    public class VehiculoController : Controller
    {
        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();
        private DataTable something;

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

            //var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);

            //if (VehiculoRegiVeSec == null)
            //{
            //    ViewData["ErrorMessage"] = ($"El Vehiculo con id: {id} no existe en la base de datos");
            //    return View("Error");
            //}
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Details([FromBody]VehiculoRegiVeSecDto vehiculoRegiVeSecDto)
        {
            var DetalleVehiculo = new VehiculoRegiVeSec();
            DetalleVehiculo.Color = vehiculoRegiVeSecDto.Color;
            DetalleVehiculo.foto = vehiculoRegiVeSecDto.foto;
            //DetalleVehiculo.ImagenesPorVehiculo = vehiculoRegiVeSecDto.ImagenesPorVehiculo;
            DetalleVehiculo.Causa = vehiculoRegiVeSecDto.Causa;
            DetalleVehiculo.Deposito = vehiculoRegiVeSecDto.Deposito;
            DetalleVehiculo.DependenciaProcedente = vehiculoRegiVeSecDto.DependenciaProcedente;
            DetalleVehiculo.Dominio = vehiculoRegiVeSecDto.Dominio;
            DetalleVehiculo.Entrega = vehiculoRegiVeSecDto.Entrega;
            DetalleVehiculo.FechaDeEntrega = Convert.ToDateTime(vehiculoRegiVeSecDto.FechaDeEntrega);
            DetalleVehiculo.FechaDeIngreso = Convert.ToDateTime(vehiculoRegiVeSecDto.FechaDeIngreso);
            DetalleVehiculo.Id = vehiculoRegiVeSecDto.Id;
            DetalleVehiculo.Marca = vehiculoRegiVeSecDto.Marca;
            DetalleVehiculo.Modelo = vehiculoRegiVeSecDto.Modelo;
            DetalleVehiculo.NumeroSumario = vehiculoRegiVeSecDto.NumeroSumario;
            DetalleVehiculo.Observaciones = vehiculoRegiVeSecDto.Observaciones;
            DetalleVehiculo.Orden = vehiculoRegiVeSecDto.Orden;
            DetalleVehiculo.Propietario = vehiculoRegiVeSecDto.Propietario;
            DetalleVehiculo.Recibe = vehiculoRegiVeSecDto.Recibe;
            DetalleVehiculo.MagistradoInterviniente = vehiculoRegiVeSecDto.MagistradoInterviniente;
            DetalleVehiculo.SumarioRegistrar = vehiculoRegiVeSecDto.SumarioRegistrar;
            DetalleVehiculo.UbicacionActual = vehiculoRegiVeSecDto.UbicacionActual;
            DetalleVehiculo.Estado = db.Estados.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Estado.Id);
            DetalleVehiculo.Tipo = db.Tipos.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Tipo.Id);
            try
            {
                //throw new Exception("No se pudo guardar el vehiculo.");
                return Redirect("/Home/Detalles");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("Error");
            }

        }

        public List<ImagenPorVehiculo> GetImagenesPorVehiculo(int idVehiculo)
        {
            var imagenes = db.ImagenPorVehiculo.Where(x => x.Vehiculo.Id == idVehiculo).ToList();

            return imagenes;
        }

        [HttpGet("/Vehiculo/GetDtoById/{id}")]
        public VehiculoRegiVeSecDto GetDtoById(int id)
        {
            var entity = GetVehiculoRegiVeSecId(id);
            var result = new VehiculoRegiVeSecDto {
                ImagenesPorVehiculo=entity.ImagenesPorVehiculo.Select(x=>x.DirecccionDeFoto).ToList(),
                Id=entity.Id,
                Causa= entity.Causa,
                Color=entity.Color,
                DependenciaProcedente= entity.DependenciaProcedente,
                Deposito= entity.Deposito,
                Dominio = entity.Dominio,
                Entrega= entity.Entrega,
               Estado= entity.Estado,
               FechaDeEntrega= Convert.ToString(entity.FechaDeEntrega),
               FechaDeIngreso= Convert.ToString(entity.FechaDeIngreso),
               MagistradoInterviniente= entity.MagistradoInterviniente,
               Marca= entity.Marca,
               Modelo= entity.Modelo,
               NumeroSumario= entity.NumeroSumario,
               Observaciones= entity.Observaciones,
                Orden =entity.Orden,
              Propietario= entity.Propietario,
              Recibe= entity.Recibe,
              SumarioRegistrar= entity.SumarioRegistrar,
              Tipo= entity.Tipo,
              
              UbicacionActual= entity.UbicacionActual
              




            };
            return result;
        }

        private void InsertImagenesPorVehiculo(List<string> imagenes,VehiculoRegiVeSec vehiculo)
        {
            foreach (var url in imagenes)
            {
                var imagen = new ImagenPorVehiculo
                {
                   DirecccionDeFoto=url,
                   Vehiculo=vehiculo
                };
                db.ImagenPorVehiculo.Add(imagen);
             
            }


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
            nuevoVehiculo.foto = vehiculoRegiVeSecDto.foto;
            //nuevoVehiculo.ImagenesPorVehiculo = vehiculoRegiVeSecDto.ImagenesPorVehiculo;
            
            nuevoVehiculo.Causa = vehiculoRegiVeSecDto.Causa;
            nuevoVehiculo.Deposito = vehiculoRegiVeSecDto.Deposito;
            nuevoVehiculo.DependenciaProcedente = vehiculoRegiVeSecDto.DependenciaProcedente;
            nuevoVehiculo.Dominio = vehiculoRegiVeSecDto.Dominio;
            nuevoVehiculo.Entrega = vehiculoRegiVeSecDto.Entrega;
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
            nuevoVehiculo.MagistradoInterviniente = vehiculoRegiVeSecDto.MagistradoInterviniente;
            nuevoVehiculo.SumarioRegistrar = vehiculoRegiVeSecDto.SumarioRegistrar;
            nuevoVehiculo.UbicacionActual = vehiculoRegiVeSecDto.UbicacionActual;
            nuevoVehiculo.Estado = db.Estados.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Estado.Id);
            nuevoVehiculo.Tipo = db.Tipos.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Tipo.Id);

           
            try
            {

                //throw new Exception("No se pudo guardar el vehiculo.");


                db.Vehiculos.Add(nuevoVehiculo);
                InsertImagenesPorVehiculo(vehiculoRegiVeSecDto.ImagenesPorVehiculo, nuevoVehiculo);
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
            InicializerEstados();
        }

        private void InicializerTipos()
        {
            var iniTipos = new InicializacionTipo(db);
            iniTipos.IniTipos();

        }
        private void InicializerEstados()
        {
            var iniEstados = new InicializadorEstados(db);
            iniEstados.IniEstados();

        }
        //[HttpPost]

        //public void getXlsxFile(something tbl, ref byte[] bytes)
        //{
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(tbl);
        //        ws.Cells["A1"].LoadFromDataTable(tbl, true);
        //        bytes = pck.GetAsByteArray();
        //    }
        //}
        //public SearchResultVehiculos Listar(int paginaActual)
        //    {
        //        //Pagina de a 10 elementos

        //        List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

        //        var vehiculosPage = db.Vehiculos
        //            .Skip((paginaActual - 1) * 5)
        //            .Take(5)
        //            .ToList();

        //        var totalRegistros = db.Vehiculos.Count();

        //        foreach (var vehiculo in vehiculosPage)
        //        {
        //            VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();
        //            dto.Id = vehiculo.Id;
        //            dto.FechaDeIngreso = vehiculo.FechaDeIngreso.ToShortDateString();
        //            dto.Propietario = vehiculo.Propietario;
        //            dto.Dominio = vehiculo.Dominio;
        //            dto.DetallesVehiculo = "Dominio: (" + vehiculo.Dominio + ") Tipo: (" + vehiculo.Tipo + ") Marca: (" + vehiculo.Marca + ") Color: (" + vehiculo.Color + ") Modelo: (" + vehiculo.Modelo + ") Estado: (" + vehiculo.Estado + ") ";
        //            dto.Tipo = vehiculo.Tipo;
        //            dto.Marca = vehiculo.Marca;
        //            dto.Color = vehiculo.Color;
        //            dto.Modelo = vehiculo.Modelo;
        //            dto.Causa = vehiculo.Causa;
        //            dto.Estado = vehiculo.Estado;
        //            dto.NumeroSumario = vehiculo.NumeroSumario;
        //            dto.Deposito = vehiculo.Deposito;
        //            dto.Orden = vehiculo.Orden;
        //            dto.DependenciaProcedente = vehiculo.DependenciaProcedente;
        //            dto.Observaciones = vehiculo.Observaciones;
        //            dto.Recibe = vehiculo.Recibe;
        //            dto.Entrega = vehiculo.Entrega;
        //dto.MagistradoInterviniente = item.MagistradoInterviniente;
        //        dto.SumarioRegistrar = item.SumarioRegistrar;
        //        dto.UbicacionActual = item.UbicacionActual;
        //            dto.FechaDeEntrega = vehiculo.FechaDeEntrega.ToShortDateString();


        //            VehiculoRegiVeSecsPrueba.Add(dto);
        //        }

        //        return new SearchResultVehiculos
        //        {
        //            Vehiculos = VehiculoRegiVeSecsPrueba,
        //            TotalRegistros = totalRegistros
        //        };


        //        //HttpContext.Session.SetString("Datos", JsonConvert.SerializeObject(VehiculoRegiVeSecsPrueba));


        //    }
        public List<VehiculoRegiVeSecDto> Listar()
        {

            List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

            foreach (var item in db.Vehiculos.Include(i => i.Tipo).ToList())
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();
               
                dto.Id = item.Id;
                dto.foto = item.foto;
           
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
                dto.Deposito = item.Deposito;
                dto.Orden = item.Orden;
                dto.DependenciaProcedente = item.DependenciaProcedente;
                dto.Observaciones = item.Observaciones;
                dto.Recibe = item.Recibe;
                dto.Entrega = item.Entrega;
                dto.MagistradoInterviniente = item.MagistradoInterviniente;
                dto.SumarioRegistrar = item.SumarioRegistrar;
                dto.UbicacionActual = item.UbicacionActual;
                dto.FechaDeEntrega = item.FechaDeEntrega.ToShortDateString();


                VehiculoRegiVeSecsPrueba.Add(dto);
            }

            VehiculoRegiVeSecsPrueba = VehiculoRegiVeSecsPrueba.OrderByDescending(x => x.Id).ToList();


            HttpContext.Session.SetString("Datos", JsonConvert.SerializeObject(VehiculoRegiVeSecsPrueba));

            return VehiculoRegiVeSecsPrueba;
        }
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
            x.Propietario.ToLower().Contains(textoABuscar) ||
            x.Causa.ToLower().Contains(textoABuscar) ||
            x.Orden.ToLower().Contains(textoABuscar))) &&
            (!filtraFecha || (x.FechaDeIngreso >= fechaDesde
            && x.FechaDeIngreso <= fechaHasta))))
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

                dto.Id = item.Id;
                dto.foto = item.foto;
           
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
                dto.Deposito = item.Deposito;
                dto.Orden = item.Orden;
                dto.DependenciaProcedente = item.DependenciaProcedente;
                dto.Observaciones = item.Observaciones;
                dto.Recibe = item.Recibe;
                dto.Entrega = item.Entrega;
                dto.MagistradoInterviniente = item.MagistradoInterviniente;
                dto.SumarioRegistrar = item.SumarioRegistrar;
                dto.UbicacionActual = item.UbicacionActual;
                dto.FechaDeEntrega = item.FechaDeEntrega.ToShortDateString();


                VehiculoRegiVeSecsPrueba.Add(dto);
            }
            return VehiculoRegiVeSecsPrueba;
        }
        //public JsonResult getEmployeeList(string sortColumnName = "FirstName", string sortOrder = "asc", int pageSize = 3, int currentPage = 1)
        //{
        //    List<VehiculoRegiVeSec> List = new List<VehiculoRegiVeSec>();
        //    int totalPage = 0;
        //    int totalRecord = 0;

        //    using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    {
        //        var emp = dc.Employees;
        //        totalRecord = emp.Count();
        //        if (pageSize > 0)
        //        {
        //            totalPage = totalRecord / pageSize + ((totalRecord % pageSize) > 0 ? 1 : 0);
        //            List = emp.OrderBy(sortColumnName + " " + sortOrder).Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();
        //        }
        //        else
        //        {
        //            List = emp.ToList();
        //        }
        //    }

        //    return new JsonResult
        //    {
        //        //Data = new { List = List, totalPage = totalPage, sortColumnName = sortColumnName, sortOrder = sortOrder, currentPage = currentPage},
        //        Data = new { List = List, totalPage = totalPage, sortColumnName = sortColumnName, sortOrder = sortOrder, currentPage = currentPage, pageSize = pageSize },
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}
        [HttpGet]
        [Route("/Vehiculo/Products")]
        
        public ActionResult Products(int page = 1, int sortBy = 1, bool isAsc = true, string search = null)
        {
            IEnumerable<VehiculoRegiVeSec> products = db.Vehiculos.Where(
                    p => search == null
                    || p.Propietario.Contains(search)
                    || p.Dominio.Contains(search)
                    || p.Causa.Contains(search));

            #region Sorting
            switch (sortBy)
            {
                case 1:
                    products = isAsc ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id);
                    break;

                case 2:
                    products = isAsc ? products.OrderBy(p => p.Propietario) : products.OrderByDescending(p => p.Propietario);
                    break;

                case 3:
                    products = isAsc ? products.OrderBy(p => p.Dominio) : products.OrderByDescending(p => p.Dominio);
                    break;

                case 4:
                    products = isAsc ? products.OrderBy(p => p.Orden) : products.OrderByDescending(p => p.Orden);
                    break;

                case 5:
                    products = isAsc ? products.OrderBy(p => p.Causa) : products.OrderByDescending(p => p.Causa);
                    break;
            }
            #endregion
            int pageSize = 10;
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / pageSize);

            products = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.SortBy = sortBy;
            ViewBag.IsAsc = isAsc;

            return View(products);
        }
        [HttpPost]
        [Route("/Vehiculo/Tabla")]
        public ActionResult<List<VehiculoRegiVeSec>> Tabla()
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int pageSize = 10;
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            var query = this.db.Vehiculos.AsQueryable();
            query = query.Skip(start).Take(pageSize);


            //public JsonResult Table([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestmodel)
            //var result = query.Skip(requestmodel.Start).Take(requestmodel.Length).Select(x => new { x.CompanyTypeName, x.CompanyTypeDescription });
            List<VehiculoRegiVeSec> empList = new List<VehiculoRegiVeSec>();

            empList = db.Vehiculos.ToList();
            int totalrows = empList.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                empList = empList.
                    Where(x => x.Marca.ToLower().Contains(searchValue.ToLower()) || x.Deposito.ToLower().Contains(searchValue.ToLower()) || x.Propietario.ToLower().Contains(searchValue.ToLower()) || x.Causa.ToString().Contains(searchValue.ToLower()) || x.Color.ToString().Contains(searchValue.ToLower())).ToList<VehiculoRegiVeSec>();
            }
            int totalrowsafterfiltering = empList.Count;

            empList = empList.Skip(start).Take(length).ToList<VehiculoRegiVeSec>();

            return new JsonResult(empList);

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
             VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Estado).FirstOrDefault(x => x.Id == id);
            //VehiculoRegiVeSec.Tipo = ;
            if (id>0)
            {
                VehiculoRegiVeSec.ImagenesPorVehiculo = GetImagenesPorVehiculo(id);
            }
          
            return VehiculoRegiVeSec;
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]VehiculoRegiVeSecDto vehiculoRegiVeSecDto)
        {
            var nuevoVehiculo = new VehiculoRegiVeSec();
            nuevoVehiculo.foto = vehiculoRegiVeSecDto.foto;
            //nuevoVehiculo.ImagenesPorVehiculo = vehiculoRegiVeSecDto.ImagenesPorVehiculo;
            nuevoVehiculo.Color = vehiculoRegiVeSecDto.Color;
            nuevoVehiculo.Causa = vehiculoRegiVeSecDto.Causa;
            nuevoVehiculo.Deposito = vehiculoRegiVeSecDto.Deposito;
            nuevoVehiculo.DependenciaProcedente = vehiculoRegiVeSecDto.DependenciaProcedente;
            nuevoVehiculo.Dominio = vehiculoRegiVeSecDto.Dominio;
            nuevoVehiculo.Entrega = vehiculoRegiVeSecDto.Entrega;
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
            nuevoVehiculo.MagistradoInterviniente = vehiculoRegiVeSecDto.MagistradoInterviniente;
            nuevoVehiculo.SumarioRegistrar = vehiculoRegiVeSecDto.SumarioRegistrar;
            nuevoVehiculo.UbicacionActual = vehiculoRegiVeSecDto.UbicacionActual;
            nuevoVehiculo.Estado = db.Estados.FirstOrDefault(x => x.Id == vehiculoRegiVeSecDto.Estado.Id);
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
            VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Estado).FirstOrDefault(x => x.Id == id);
            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El VehiculoRegiVeSec con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
        }
        public IEnumerable<VehiculoRegiVeSec> Listaa()
        {
            var Vehiculos = db.Vehiculos.Include("Estado").ToList();

            return Vehiculos;
        }
        [HttpGet]
        [Route("/Vehiculo/GetEstados")]
        public List<Estado> GetEstados()
        {
            var result = db.Estados;
            return result.ToList();
            //return db.Contactos;
        }
        public List<Estado> ObtenerTodoss()
        {
            using (var db = new Conexionbd())
            {
                return db.Estados.ToList();
            }
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
