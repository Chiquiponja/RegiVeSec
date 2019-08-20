using Microsoft.AspNetCore.Identity;
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
            public DbSet<Estado> Estados { get; set; }
            public DbSet<Tipo> Tipos { get; set; }
            public DbSet<Login> Logins { get; set; }
            public DbSet<IdentityUser> IdentityUser { get; set; }
            public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }

            //public DbSet<IdentityUserToken<string>> IdentityUserToken { get; set; }

    }
}


