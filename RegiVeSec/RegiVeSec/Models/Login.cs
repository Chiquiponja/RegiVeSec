using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Models
{
    public class Login
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Contrasenia { get; set; }
        public Boolean Estado { get; set; }
        public ICollection<VehiculoRegiVeSec> vehiculoRegiVeSecs { get; set; }
    }
}
