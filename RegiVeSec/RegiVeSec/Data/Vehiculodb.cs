using Microsoft.EntityFrameworkCore;
using RegiVeSec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Data
{
    
        public class Conexionbd : DbContext
        {
            internal object VehiculoRegiVeSec;
            public Conexionbd()
            {
            }
            public Conexionbd(DbContextOptions<Conexionbd> options)
                    : base(options)
            { }
            public DbSet<VehiculoRegiVeSec> Vehiculos { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
    
}
