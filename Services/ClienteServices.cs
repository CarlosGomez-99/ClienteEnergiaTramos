using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergiaElectrica.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergiaElectrica.Services
{
    public class ClienteServices : IClienteService
    {
        EnergiaContext context;

        public ClienteServices(EnergiaContext dbcontext)
        {
            context = dbcontext;
        }

        public List<Cliente> GetClientes(string fechaInicial, string fechaFinal)
        {
            DateTime fechaIni = DateTime.Parse(fechaInicial);
            DateTime fechaFin = DateTime.Parse(fechaFinal);
            return context.Cliente
            .Select(a => new Cliente
            {
                ClienteId = a.ClienteId,
                Nombre = a.Nombre,
                Consumos = context.Consumos.Where(p => p.ClienteId == a.ClienteId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Select(p => new Consumos
                {
                    ConsumoId = p.ConsumoId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Tramo = new Tramo() { Nombre = context.Tramo.Where(a => a.TramoId == p.TramoId).FirstOrDefault().Nombre }
                }).ToList(),
                Costos = context.Costos.Where(p => p.ClienteId == a.ClienteId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Select(p => new Costos
                {
                    CostoId = p.CostoId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Tramo = new Tramo() { Nombre = context.Tramo.Where(a => a.TramoId == p.TramoId).FirstOrDefault().Nombre }
                }).ToList(),
                Perdidas = context.Perdidas.Where(p => p.ClienteId == a.ClienteId).Where(p => p.Fecha >= fechaIni && p.Fecha <= fechaFin).Select(p => new Perdidas
                {
                    PerdidaId = p.PerdidaId,
                    Fecha = p.Fecha,
                    Valor = p.Valor,
                    Tramo = new Tramo() { Nombre = context.Tramo.Where(a => a.TramoId == p.TramoId).FirstOrDefault().Nombre }
                }).ToList()
            }).ToList();
        }
    }
    public interface IClienteService
    {
        List<Cliente> GetClientes(string fechaInicial, string fechaFinal);
    }
}