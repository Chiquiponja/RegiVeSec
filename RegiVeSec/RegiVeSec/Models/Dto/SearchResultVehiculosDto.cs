using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Models.Dto
{
    public class SearchResultVehiculos
    {
        public List<VehiculoRegiVeSecDto> Vehiculos { get; set; }
        public int TotalRegistros { get; set; }
    }
}
