using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EnergiaElectrica.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergiaElectrica
{
    public class EnergiaContext : DbContext
    {
        public DbSet<Tramo> Tramo { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Consumos> Consumos { get; set; }
        public DbSet<Costos> Costos { get; set; }
        public DbSet<Perdidas> Perdidas { get; set; }

        public EnergiaContext(DbContextOptions<EnergiaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Tramo> tramoInit = new List<Tramo>()
            {
                new Tramo() { TramoId = 1, Nombre = "Tramo 1" },
                new Tramo() { TramoId = 2, Nombre = "Tramo 2" },
                new Tramo() { TramoId = 3, Nombre = "Tramo 3" },
                new Tramo() { TramoId = 4, Nombre = "Tramo 4" },
                new Tramo() { TramoId = 5, Nombre = "Tramo 5" }
            };
            modelBuilder.Entity<Tramo>(tramo =>
            {
                tramo.ToTable("Tramo");
                tramo.HasKey(e => e.TramoId);
                tramo.Property(e => e.Nombre).HasMaxLength(50);
                tramo.HasData(tramoInit);
            });
            List<Cliente> clientesInit = new List<Cliente>()
            {
                new Cliente() { ClienteId = 1, Nombre = "Residencial" },
                new Cliente() { ClienteId = 2, Nombre = "Comercial" },
                new Cliente() { ClienteId = 3, Nombre = "Industrial" }
            };
            modelBuilder.Entity<Cliente>(tramo =>
            {
                tramo.ToTable("Cliente");
                tramo.HasKey(e => e.ClienteId);
                tramo.Property(e => e.Nombre).HasMaxLength(50);
                tramo.HasData(clientesInit);
            });
            List<Consumos> consumosInit = new List<Consumos>();
            var dt = GetTableConsumoTramo("CONSUMO_POR_TRAMO");
            int idConsumo = 1;
            foreach (DataRow item in dt.Rows)
            {
                for (int i = 2; i <= 4; i++)
                {
                    consumosInit.Add(
                        new Consumos()
                        {
                            ConsumoId = idConsumo,
                            Fecha = Convert.ToDateTime(item.Field<string>(1)),
                            TramoId = tramoInit.Find(e => e.Nombre == item.Field<string>(0)).TramoId,
                            ClienteId = i == 2 ? clientesInit.Find(e => e.Nombre == "Residencial").ClienteId : i == 3 ? clientesInit.Find(e => e.Nombre == "Comercial").ClienteId : clientesInit.Find(e => e.Nombre == "Industrial").ClienteId,
                            Valor = Convert.ToInt32(item.Field<string>(i))
                        }
                    );
                    idConsumo++;
                }
            }
            modelBuilder.Entity<Consumos>(consumo =>
                {
                    consumo.ToTable("Consumo");
                    consumo.HasKey(e => e.ConsumoId);
                    consumo.HasOne(e => e.Tramo).WithMany(e => e.Consumos).HasForeignKey(e => e.TramoId);
                    consumo.HasOne(e => e.Cliente).WithMany(e => e.Consumos).HasForeignKey(e => e.ClienteId);
                    consumo.Property(e => e.Fecha);
                    consumo.Property(e => e.Valor);
                    consumo.HasData(consumosInit);
                });
            int idCosto = 1;
            var dtCosto = GetTableConsumoTramo("COSTOS_POR_TRAMO");
            List<Costos> costosInit = new List<Costos>();
            foreach (DataRow item in dtCosto.Rows)
            {
                for (int i = 2; i <= 4; i++)
                {
                    costosInit.Add(
                        new Costos()
                        {
                            CostoId = idCosto,
                            Fecha = Convert.ToDateTime(item.Field<string>(1)),
                            TramoId = tramoInit.Find(e => e.Nombre == item.Field<string>(0)).TramoId,
                            ClienteId = i == 2 ? clientesInit.Find(e => e.Nombre == "Residencial").ClienteId : i == 3 ? clientesInit.Find(e => e.Nombre == "Comercial").ClienteId : clientesInit.Find(e => e.Nombre == "Industrial").ClienteId,
                            Valor = Convert.ToDouble(item.Field<string>(i))
                        }
                    );
                    idCosto++;
                }
            }
            modelBuilder.Entity<Costos>(costos =>
                    {
                        costos.ToTable("Costos");
                        costos.HasKey(e => e.CostoId);
                        costos.HasOne(e => e.Tramo).WithMany(e => e.Costos).HasForeignKey(e => e.TramoId);
                        costos.HasOne(e => e.Cliente).WithMany(e => e.Costos).HasForeignKey(e => e.ClienteId);
                        costos.Property(e => e.Fecha);
                        costos.Property(e => e.Valor);
                        costos.HasData(costosInit);
                    });
            int idPerdidas = 1;
            var dtPerdida = GetTableConsumoTramo("PERDIDAS_POR_TRAMO");
            List<Perdidas> perdidasInit = new List<Perdidas>();
            foreach (DataRow item in dtPerdida.Rows)
            {
                for (int i = 2; i <= 4; i++)
                {
                    perdidasInit.Add(
                        new Perdidas()
                        {
                            PerdidaId = idPerdidas,
                            Fecha = Convert.ToDateTime(item.Field<string>(1)),
                            TramoId = tramoInit.Find(e => e.Nombre == item.Field<string>(0)).TramoId,
                            ClienteId = i == 2 ? clientesInit.Find(e => e.Nombre == "Residencial").ClienteId : i == 3 ? clientesInit.Find(e => e.Nombre == "Comercial").ClienteId : clientesInit.Find(e => e.Nombre == "Industrial").ClienteId,
                            Valor = Convert.ToDouble(item.Field<string>(i))
                        }
                    );
                    idPerdidas++;
                }
            }
            modelBuilder.Entity<Perdidas>(costos =>
            {
                costos.ToTable("Perdidas");
                costos.HasKey(e => e.PerdidaId);
                costos.HasOne(e => e.Tramo).WithMany(e => e.Perdidas).HasForeignKey(e => e.TramoId);
                costos.HasOne(e => e.Cliente).WithMany(e => e.Perdidas).HasForeignKey(e => e.ClienteId);
                costos.Property(e => e.Fecha);
                costos.Property(e => e.Valor);
                costos.HasData(perdidasInit);
            });
        }


        private DataTable GetTableConsumoTramo(string hojaExcel)
        {
            DataTable DT = new();

            string startupPath = Path.Combine(Environment.CurrentDirectory, "Archivos/EPSA_Listado_Resumen.xlsx");


            using var woorbook = new XLWorkbook(startupPath);

            var wookSheet = woorbook.Worksheet(hojaExcel);

            bool firstRow = true;
            foreach (IXLRow row in wookSheet.Rows())
            {
                if (firstRow)
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        DT.Columns.Add(cell.Value.ToString());
                    }
                    firstRow = false;
                }
                else
                {
                    DT.Rows.Add();
                    int i = 0;
                    foreach (IXLCell cell in row.Cells())
                    {
                        DT.Rows[DT.Rows.Count - 1][i] = cell.Value.ToString();
                        i++;
                    }
                }
            }


            return DT;

        }
    }

}