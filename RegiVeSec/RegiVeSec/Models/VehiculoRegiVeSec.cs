﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegiVeSec.Models
{
    public class VehiculoRegiVeSec
    {
        public int Id { get; set; }
        public DateTime FechaDeIngreso { get; set; }
        public string Propietario { get; set; }
        public string Dominio  { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Color  { get; set; }
        public string Modelo { get; set; }
        public string Causa { get; set; }
        public string Estado { get; set; }
        public string NumeroSumario { get; set; }
        public string Dependencia  { get; set; }
        public string Orden  { get; set; }
        public string DependenciaProcedente { get; set; }
        public string Observaciones { get; set; }
        public string Recibe  { get; set; }
        public string Entrega { get; set; }
        public DateTime FechaDeEntrega { get; set; }


    }
}