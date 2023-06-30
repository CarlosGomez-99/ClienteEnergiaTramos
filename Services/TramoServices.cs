using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergiaElectrica.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergiaElectrica.Services
{
    public class TramoServices : ITramoServices
    {
        EnergiaContext context;
        public TramoServices(EnergiaContext dbcontext)
        {
            context = dbcontext;
        }

        public List<Tramo> GetPeoresTramos(string fechaInicial, string fechaFinal, int TramosPerdidas)
        {
            DateTime fechaIni = DateTime.Parse(fechaInicial);
            DateTime fechaFin = DateTime.Parse(fechaFinal);
            return context.Tramo
            .Select(a => new Tramo()
            {
                TramoId = a.TramoId,
                Nombre = a.Nombre,
                Perdidas = context.Perdidas.Where(p => p.TramoId == a.TramoId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Take(TramosPerdidas).OrderByDescending(p => p.Valor).Select(p => new Perdidas
                {
                    PerdidaId = p.PerdidaId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Cliente = new Cliente() { Nombre = context.Cliente.Where(a => a.ClienteId == p.ClienteId).FirstOrDefault().Nombre }
                }).ToList()
            }).ToList();
        }

        public List<Tramo> GetTramos(string fechaInicial, string fechaFinal)
        {
            DateTime fechaIni = DateTime.Parse(fechaInicial);
            DateTime fechaFin = DateTime.Parse(fechaFinal);
            return context.Tramo
            .Select(a => new Tramo()
            {
                TramoId = a.TramoId,
                Nombre = a.Nombre,
                Consumos = context.Consumos.Where(p => p.TramoId == a.TramoId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Select(p => new Consumos
                {
                    ConsumoId = p.ConsumoId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Cliente = new Cliente() { Nombre = context.Cliente.Where(a => a.ClienteId == p.ClienteId).FirstOrDefault().Nombre }
                }).ToList(),
                Costos = context.Costos.Where(p => p.TramoId == a.TramoId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Select(p => new Costos
                {
                    CostoId = p.CostoId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Cliente = new Cliente() { Nombre = context.Cliente.Where(a => a.ClienteId == p.ClienteId).FirstOrDefault().Nombre }
                }).ToList(),
                Perdidas = context.Perdidas.Where(p => p.TramoId == a.TramoId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Select(p => new Perdidas
                {
                    PerdidaId = p.PerdidaId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Cliente = new Cliente() { Nombre = context.Cliente.Where(a => a.ClienteId == p.ClienteId).FirstOrDefault().Nombre }
                }).ToList()
            }).ToList();
        }
    }
    public interface ITramoServices
    {
        List<Tramo> GetTramos(string fechaInicial, string fechaFinal);
        List<Tramo> GetPeoresTramos(string fechaInicial, string fechaFinal, int TramosPerdidas);
    }
}