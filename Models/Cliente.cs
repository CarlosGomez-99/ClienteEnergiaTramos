using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnergiaElectrica.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public virtual List<Consumos> Consumos { get; set; }
        public virtual List<Costos> Costos { get; set; }
        public virtual List<Perdidas> Perdidas { get; set; }
    }
}