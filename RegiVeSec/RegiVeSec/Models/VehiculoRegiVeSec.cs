using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RegiVeSec.Models.Dto;

namespace RegiVeSec.Models
{
    public class VehiculoRegiVeSec
    {
        public int Id { get; set; }
        public DateTime FechaDeIngreso { get; set; }

        [MaxLength(30)]
        [Required]
        public string Propietario { get; set; }

        [MaxLength(30)]
        [Required]
        public string Dominio { get; set; }

        [MaxLength(30)]
        [Required]

        public Tipo Tipo { get; set; }

        [MaxLength(30)]
        [Required]
        public string Marca { get; set; }

        [MaxLength(30)]
        [Required]
        public string Color { get; set; }

        [MaxLength(30)]
        [Required]
        public string Modelo { get; set; }

        [MaxLength(30)]
        [Required]
        public string Causa { get; set; }

        [MaxLength(30)]
        [Required]
        public Estado Estado { get; set; }

        [MaxLength(30)]
        [Required]
        public string NumeroSumario { get; set; }

        [MaxLength(30)]
        [Required]
        public string Deposito { get; set; }

        [MaxLength(30)]
        [Required]
        public string Orden { get; set; }

        [MaxLength(30)]
        [Required]
        public string DependenciaProcedente { get; set; }

        [MaxLength(30)]
        [Required]
        public string Observaciones { get; set; }

        [MaxLength(30)]
        [Required]
        public string Recibe { get; set; }

        [MaxLength(30)]
        [Required]
        public string MagistradoInterviniente { get; set; }
        [MaxLength(30)]
        [Required]
        public string SumarioRegistrar { get; set; }
        [MaxLength(30)]
        [Required]
        public string UbicacionActual { get; set; }
        [MaxLength(30)]
        //  //  [Required]
        public string Entrega { get; set; }

        public string foto { get; set; }
        public ICollection<ImagenPorVehiculo> ImagenesPorVehiculo { get; set; }
        public DateTime FechaDeEntrega { get; set; }

        internal static object GetOne()
        {
            throw new NotImplementedException();
        }
    }
}

