using RegiVeSec.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace RegiVeSec.Data
{
    public class InicializacionTipo
    {
        private Conexionbd db = new Conexionbd();
        public SqlConnection conectarbd = new SqlConnection();

        public InicializacionTipo(Conexionbd _db)
        {
            db = _db;
        }


        public void IniTipos()
        {
            var existentes = db.Tipos.ToList();
            var nuevos = new List<Tipo>(){
        new Tipo{Detalles="Bicicleta"},
        new Tipo{Detalles ="Moto"},
        new Tipo{Detalles="Auto"},
        new Tipo{Detalles = "Camion"},
        new Tipo{Detalles="Colectivo"},
        new Tipo{Detalles="Unitario"}
      };

            foreach (var item in nuevos.Where(x => existentes.All(f => f.Detalles != x.Detalles)))
            {
                db.Tipos.Add(item);
                db.SaveChangesAsync();
            }

        }

    }
}