using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Models.Dto
{
    public class ImagenPorVehiculo
    {
        public int Id { get; set; }
        public VehiculoRegiVeSec Vehiculo { get; set; }

        public string DirecccionDeFoto { get; set; }
    }
}
