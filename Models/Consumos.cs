using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnergiaElectrica.Models
{
    public class Consumos
    {
        public int ConsumoId { get; set; }
        public int TramoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public int Valor { get; set; }
        public virtual Tramo Tramo { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}