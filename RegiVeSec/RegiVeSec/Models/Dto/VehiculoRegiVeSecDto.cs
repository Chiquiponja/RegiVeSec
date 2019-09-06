using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Models
{
    public class VehiculoRegiVeSecDto
    {
        public int Id { get; set; }
        public string FechaDeIngreso { get; set; }
        public string Propietario { get; set; }
        public string Dominio  { get; set; }
        public Tipo Tipo { get; set; }
        public string Marca { get; set; }
        public string Color  { get; set; }
        public string Modelo { get; set; }
        public string Causa { get; set; }
        public Estado Estado { get; set; }
        public string NumeroSumario { get; set; }
        public string Deposito { get; set; }
        public string Orden  { get; set; }
        public string DependenciaProcedente { get; set; }
        public string Observaciones { get; set; }
        public string Recibe  { get; set; }
        public string Entrega { get; set; }
        public string FechaDeEntrega { get; set; }
        public string DetallesVehiculo { get; set; }
        public string MagistradoInterviniente { get; set; }
        public string SumarioRegistrar { get; set; }
        public string UbicacionActual { get; set; }
        public int TipoId { get; set; }
        public int EstadoId { get; set; }
     
  }
}
