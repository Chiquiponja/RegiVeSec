using RegiVeSec.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Data
{
    public class InicializadorEstados
    {
        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();

        public InicializadorEstados(Conexionbd _db)
        {
            db = _db;
        }


        public void IniEstados()
        {
            var existentes = db.Estados.ToList();
            var nuevos = new List<Estado>(){
        new Estado{Detalles ="Moto"},
        new Estado{Detalles="Auto"},
        new Estado{Detalles = "Camion"},
        new Estado{Detalles="Camioneta"}
      };

            foreach (var item in nuevos.Where(x => existentes.All(f => f.Detalles != x.Detalles)))
            {
                db.Estados.Add(item);
                db.SaveChangesAsync();
            }

        }
    }
}
