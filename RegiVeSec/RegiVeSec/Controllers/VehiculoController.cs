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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Html;
using System.Net.Http;
using RegiVeSec.Models.Dto;

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

       

        public ActionResult ExportToPdf(DataTable dt)
        {
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = PdfWriter.GetInstance(document, ms);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
             
            
            PdfPTable tblPrueba = new PdfPTable(8);
            tblPrueba.WidthPercentage = 100;
            document.Open();
            var parrafo = new Paragraph(string.Format(" Archivo {0}  ", DateTime.Now.ToShortDateString()));
            parrafo.SpacingBefore = 5;
            parrafo.SpacingAfter = 2;
            parrafo.Alignment = 2; //0-Left, 1 middle,2 Right
            document.Add(parrafo);
            document.Add(Chunk.NEWLINE);
            var parrafo2 = new Paragraph("REGISTRO DE VEHICULOS SINESTRADOS");
            parrafo2.SpacingBefore = 1;
            parrafo2.SpacingAfter = 0;
            parrafo2.Alignment = 1; //0-Left, 1 middle,2 Right
            document.Add(parrafo2);
            document.Add(Chunk.NEWLINE);
           


            PdfPCell clFechadeIngreso = new PdfPCell(new Phrase("FECHA DE INGRESO", _standardFont));
            clFechadeIngreso.BorderWidthTop = 1;
            clFechadeIngreso.BorderWidthBottom = 1f;
            clFechadeIngreso.BackgroundColor = BaseColor.BLACK;
            PdfPCell clNumerodeSumario = new PdfPCell(new Phrase("NUMERO DE SUMARIO", _standardFont));
            clNumerodeSumario.BorderWidthLeft= 1;
            clNumerodeSumario.BorderWidthBottom = 1f;
            clNumerodeSumario.BackgroundColor = BaseColor.BLACK;
            PdfPCell clTipo = new PdfPCell(new Phrase("TIPO", _standardFont));
            clTipo.BorderWidthRight = 1;
            clTipo.BorderWidthBottom = 1f;
            clTipo.BackgroundColor = BaseColor.BLACK;
            PdfPCell clMarca = new PdfPCell(new Phrase("MARCA", _standardFont));
            clMarca.BorderWidthTop = 1;
            clMarca.BorderWidthBottom = 1f;
            clMarca.BackgroundColor = BaseColor.BLACK;
            PdfPCell clDominio = new PdfPCell(new Phrase("DOMINIO", _standardFont));
            clDominio.BorderWidthLeft = 1;
            clDominio.BorderWidthBottom = 1f;
            clDominio.BackgroundColor = BaseColor.BLACK;
            PdfPCell clOrden = new PdfPCell(new Phrase("ORDEN", _standardFont));
            clOrden.BorderWidthRight = 1;
            clOrden.BorderWidthBottom = 1f;
            clOrden.BackgroundColor = BaseColor.BLACK;
            PdfPCell clCausa = new PdfPCell(new Phrase("CAUSA", _standardFont));
            clCausa.BorderWidthTop = 1;
            clCausa.BorderWidthBottom = 1f;
            clCausa.BackgroundColor = BaseColor.BLACK;
            PdfPCell clFechaDeEntrega = new PdfPCell(new Phrase("FECHA DE ENTREGA", _standardFont));
            clFechaDeEntrega.BorderWidthLeft = 1;
            clFechaDeEntrega.BorderWidthBottom = 1f;
            clFechaDeEntrega.BackgroundColor = BaseColor.BLACK;


            tblPrueba.AddCell(clFechadeIngreso);
            tblPrueba.AddCell(clNumerodeSumario);
            tblPrueba.AddCell(clTipo);
            tblPrueba.AddCell(clMarca);
            tblPrueba.AddCell(clDominio);
            tblPrueba.AddCell(clOrden);
            tblPrueba.AddCell(clCausa);
            tblPrueba.AddCell(clFechaDeEntrega);

            List<VehiculoRegiVeSec> vehiculos = db.Vehiculos.Include(i => i.Tipo).OrderByDescending(x => x.FechaDeIngreso).ToList();
            foreach (var item in vehiculos)
            {
                tblPrueba.AddCell(item.FechaDeIngreso.ToShortDateString());
                tblPrueba.AddCell(item.NumeroSumario);
                tblPrueba.AddCell(item.Tipo.Detalles);
                tblPrueba.AddCell(item.Marca);
                tblPrueba.AddCell(item.Dominio);
                tblPrueba.AddCell(item.Orden);
                tblPrueba.AddCell(item.Causa);
                tblPrueba.AddCell(item.FechaDeEntrega.ToShortDateString());
            }
            tblPrueba.DefaultCell.Padding = 30;
            tblPrueba.WidthPercentage = 100;
            tblPrueba.HorizontalAlignment = Element.ALIGN_LEFT;
            tblPrueba.DefaultCell.BorderWidth = 1;
            document.Add(tblPrueba);
            document.Close();
            byte[] bytesStrem = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStrem, 0, bytesStrem.Length);
            ms.Position = 0;
           
                return new FileStreamResult(ms, "aplication/pdf")
                {
                    FileDownloadName = string.Format("RegiVeSec Archivo {0}.pdf", DateTime.Now.ToShortDateString())
                };
            
        }
        public IActionResult ExportToPdfDetalles(int id)
        {
            
            List<VehiculoRegiVeSec> vehiculos = db.Vehiculos.Include(i => i.Tipo).Include(i => i.Tipo).Where(x => x.Id == id).ToList();
           
            ViewData["Id"] = id;
            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);
            VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Tipo).FirstOrDefault(x => x.Id == id);
            VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Estado).FirstOrDefault(x => x.Id == id);
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = PdfWriter.GetInstance(document, ms);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


            PdfPTable tblPrueba = new PdfPTable(2);
            tblPrueba.WidthPercentage = 100;
            document.Open();
            var parrafo = new Paragraph(string.Format(" Archivo {0}  ", DateTime.Now.ToShortDateString()));
            parrafo.SpacingBefore = 5;
            parrafo.SpacingAfter = 2;
            parrafo.Alignment = 2; //0-Left, 1 middle,2 Right
            document.Add(parrafo);
            document.Add(Chunk.NEWLINE);
            var parrafo2 = new Paragraph("REGISTRO DE VEHICULOS SINESTRADOS");
            parrafo2.SpacingBefore = 1;
            parrafo2.SpacingAfter = 0;
            parrafo2.Alignment = 1; //0-Left, 1 middle,2 Right
            document.Add(parrafo2);
            document.Add(Chunk.NEWLINE);


            foreach (var item in vehiculos)
            {
                PdfPCell clFechadeIngreso = new PdfPCell(new Phrase("Fecha de Ingreso", _standardFont));
                clFechadeIngreso.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clFechadeIngreso);
                tblPrueba.AddCell(item.FechaDeIngreso.ToShortDateString());
                PdfPCell clNumerodeSumario = new PdfPCell(new Phrase("Numero Sumario", _standardFont));
                clNumerodeSumario.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clNumerodeSumario);
                tblPrueba.AddCell(item.NumeroSumario);
                PdfPCell clTipo = new PdfPCell(new Phrase("Tipo", _standardFont));
                clTipo.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clTipo);
                tblPrueba.AddCell(item.Tipo.Detalles);
                PdfPCell clMarca = new PdfPCell(new Phrase("Marca", _standardFont));
                clMarca.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clMarca);
                tblPrueba.AddCell(item.Marca);
                PdfPCell clDominio = new PdfPCell(new Phrase("Dominio", _standardFont));
                clDominio.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clDominio);
                tblPrueba.AddCell(item.Dominio);
                PdfPCell clOrden = new PdfPCell(new Phrase("Orden", _standardFont));
                clOrden.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clOrden);
                tblPrueba.AddCell(item.Orden);
                PdfPCell clCausa = new PdfPCell(new Phrase("Causa", _standardFont));
                clCausa.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clCausa);
                tblPrueba.AddCell(item.Causa);
                PdfPCell clSumarioRegistrar = new PdfPCell(new Phrase("SumarioRegistrar", _standardFont));
                clSumarioRegistrar.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clSumarioRegistrar);
                tblPrueba.AddCell(item.SumarioRegistrar);

                PdfPCell clColor = new PdfPCell(new Phrase("Color", _standardFont));
                clColor.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clColor);
                tblPrueba.AddCell(item.Color);

                PdfPCell clMagistradoInterviniente = new PdfPCell(new Phrase("Magistrado Interviniente", _standardFont));
                clMagistradoInterviniente.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clMagistradoInterviniente);
                tblPrueba.AddCell(item.MagistradoInterviniente);


                PdfPCell clDependenciaProcedente = new PdfPCell(new Phrase("DependenciaProcedente", _standardFont));
                clDependenciaProcedente.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clDependenciaProcedente);
                tblPrueba.AddCell(item.DependenciaProcedente);

                PdfPCell clEstado = new PdfPCell(new Phrase("Estado", _standardFont));
                clEstado.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clEstado);
                tblPrueba.AddCell(item.Estado.Detalles);

                PdfPCell clRecibe = new PdfPCell(new Phrase("Recibe", _standardFont));
                clRecibe.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clRecibe);
                tblPrueba.AddCell(item.Recibe);

                PdfPCell clEntrega = new PdfPCell(new Phrase("Entrega", _standardFont));
                clEntrega.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clEntrega);
                tblPrueba.AddCell(item.Entrega);

                PdfPCell clUbicacionActual = new PdfPCell(new Phrase("Ubicacion Actual", _standardFont));
                clUbicacionActual.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clUbicacionActual);
                tblPrueba.AddCell(item.UbicacionActual);

                PdfPCell clPropietario = new PdfPCell(new Phrase("Propietario", _standardFont));
                clPropietario.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clPropietario);
                tblPrueba.AddCell(item.Propietario);


                PdfPCell clDeposito = new PdfPCell(new Phrase("Deposito", _standardFont));
                clDeposito.BackgroundColor = BaseColor.GRAY;
                tblPrueba.AddCell(clDeposito);
                tblPrueba.AddCell(item.Deposito);

                //PdfPCell clOrden = new PdfPCell(new Phrase("Orden", _standardFont));
                //clOrden.BackgroundColor = BaseColor.WHITE;
                //tblPrueba.AddCell(clOrden);
                //tblPrueba.AddCell(item.Orden);

                PdfPCell clFechaDeEntrega = new PdfPCell(new Phrase("Fecha de Entrega", _standardFont));
                clFechaDeEntrega.BackgroundColor = BaseColor.WHITE;
                tblPrueba.AddCell(clFechaDeEntrega);
            
                tblPrueba.AddCell(item.FechaDeEntrega.ToShortDateString());
            }
            //tblPrueba.DefaultCell.Padding = 30;
            //tblPrueba.WidthPercentage = 100;
            //tblPrueba.HorizontalAlignment = Element.ALIGN_LEFT;
            //tblPrueba.DefaultCell.BorderWidth = 1;
            document.Add(tblPrueba);
            document.Close();
            byte[] bytesStrem = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStrem, 0, bytesStrem.Length);
            ms.Position = 0;

            

            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El Vehiculo con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return new FileStreamResult(ms, "aplication/pdf")
            {
                FileDownloadName = string.Format("RegiVeSec Archivo {0}.pdf", DateTime.Now.ToShortDateString())
            };
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
                //InsertImagenesPorVehiculo(vehiculoRegiVeSecDto.ImagenesPorVehiculo, nuevoVehiculo);
                UpdateImagenes(nuevoVehiculo, vehiculoRegiVeSecDto.ImagenesPorVehiculo);
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

        public SearchResultVehiculos Listar(int paginaActual)
        {
            List<VehiculoRegiVeSecDto> VehiculoRegiVeSecsPrueba = new List<VehiculoRegiVeSecDto>();

            var totalRegistros = db.Vehiculos.Count();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)10);

            var vehiculosPage = db.Vehiculos.Include(i => i.Tipo)
                .OrderByDescending(x => x.FechaDeIngreso)
                .Skip((paginaActual - 1) * 10)
                .Take(10)
                .ToList();

            foreach (var vehiculo in vehiculosPage )
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();
                dto.Id = vehiculo.Id;
                dto.FechaDeIngreso = vehiculo.FechaDeIngreso.ToShortDateString();
                dto.Propietario = vehiculo.Propietario;
                dto.Dominio = vehiculo.Dominio;
                dto.DetallesVehiculo = "Dominio: (" + vehiculo.Dominio + ") Tipo: (" + vehiculo.Tipo + ") Marca: (" + vehiculo.Marca + ") Color: (" + vehiculo.Color + ") Modelo: (" + vehiculo.Modelo + ") Estado: (" + vehiculo.Estado + ") ";
                dto.Tipo = vehiculo.Tipo;
                dto.Marca = vehiculo.Marca;
                dto.Color = vehiculo.Color;
                dto.Modelo = vehiculo.Modelo;
                dto.Causa = vehiculo.Causa;
                dto.Estado = vehiculo.Estado;
                dto.NumeroSumario = vehiculo.NumeroSumario;
                dto.Deposito = vehiculo.Deposito;
                dto.Orden = vehiculo.Orden;
                dto.DependenciaProcedente = vehiculo.DependenciaProcedente;
                dto.Observaciones = vehiculo.Observaciones;
                dto.Recibe = vehiculo.Recibe;
                dto.Entrega = vehiculo.Entrega;
                dto.MagistradoInterviniente = vehiculo.MagistradoInterviniente;
                dto.SumarioRegistrar = vehiculo.SumarioRegistrar;
                dto.UbicacionActual = vehiculo.UbicacionActual;
                dto.FechaDeEntrega = vehiculo.FechaDeEntrega.ToShortDateString();

                VehiculoRegiVeSecsPrueba.Add(dto);
            }
             

            return new SearchResultVehiculos
            {
                Vehiculos = VehiculoRegiVeSecsPrueba,
                TotalRegistros = totalRegistros,
                TotalPaginas= totalPaginas
            };


            //HttpContext.Session.SetString("Datos", JsonConvert.SerializeObject(VehiculoRegiVeSecsPrueba));


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

            foreach (var item in db.Vehiculos.Include(i => i.Tipo).Where(x =>
            ( !filtraTexto || (x.NumeroSumario.ToLower().Contains(textoABuscar) ||
            x.Tipo.Detalles.ToLower().Contains(textoABuscar) ||
            x.Marca.ToLower().Contains(textoABuscar) ||
            x.Dominio.ToLower().Contains(textoABuscar) ||
            x.Orden.ToLower().Contains(textoABuscar) ||
            x.Causa.ToLower().Contains(textoABuscar))) &&
            (!filtraFecha || (x.FechaDeIngreso >= fechaDesde
            && x.FechaDeIngreso <= fechaHasta))))
            {
                VehiculoRegiVeSecDto dto = new VehiculoRegiVeSecDto();

                dto.Id = item.Id;
                dto.foto = item.foto;
           
                dto.FechaDeIngreso = item.FechaDeIngreso.ToShortDateString();
                dto.Propietario = item.Propietario;
                dto.Dominio = item.Dominio;
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

        public IActionResult Eliminar(int id)
        {
            ViewData["Id"] = id;
            var VehiculoRegiVeSec = GetVehiculoRegiVeSecId(id);
            VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Tipo).FirstOrDefault(x => x.Id == id);
            VehiculoRegiVeSec = db.Vehiculos.Include(i => i.Estado).FirstOrDefault(x => x.Id == id);

            if (VehiculoRegiVeSec == null)
            {
                ViewData["ErrorMessage"] = ($"El Vehiculo con id: {id} no existe en la base de datos");
                return View("Error");
            }
            return View();
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

        private void UpdateImagenes(VehiculoRegiVeSec vehiculo,List<string> imagenes ) 
        {
         
            var imagenesGuardadas = db.ImagenPorVehiculo.Where(x=>x.Vehiculo.Id==vehiculo.Id).ToList();
            var imgenesBorrar = imagenesGuardadas.Where( x=> imagenes.All(f=> f!=x.DirecccionDeFoto));
            var imagenesNuevas =imagenes.Where(x=> imagenesGuardadas.All(f=>f.DirecccionDeFoto!=x));


            
            foreach (var item in imgenesBorrar)
            {
                db.ImagenPorVehiculo.Remove(item);
                
            }


            foreach (var item in imagenesNuevas)
            {
                db.ImagenPorVehiculo.Add(new ImagenPorVehiculo
                {
                   DirecccionDeFoto=item,
                   Vehiculo=vehiculo
                });
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
                UpdateImagenes(nuevoVehiculo, vehiculoRegiVeSecDto.ImagenesPorVehiculo);
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
