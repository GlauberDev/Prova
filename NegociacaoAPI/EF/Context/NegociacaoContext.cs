using Microsoft.EntityFrameworkCore;
using NegociacaoAPI.Models;
using System;
using System.Collections.Generic;

namespace NegociacaoAPI.EF
{
    public class NegociacaoContext : DbContext
    {
        public NegociacaoContext(DbContextOptions<NegociacaoContext> options) : base(options)
        {
        }
        public DbSet<Tomador> tomadores { get; set; }
        public DbSet<Divida> dividas { get; set; }
        public DbSet<Simulacao> simulacoes { get; set; }
        public DbSet<Acordo> acordos { get; set; }
        public DbSet<Parcela> parcelamentos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tomador>().HasData
                (
                    new Tomador() { TomadorId = 1, CPF = "987.335.450-65" },
                    new Tomador() { TomadorId = 2, CPF = "245.546.680-96" },
                    new Tomador() { TomadorId = 3, CPF = "435.964.560-02" },
                    new Tomador() { TomadorId = 4, CPF = "940.872.480-11" }
                );

            modelBuilder.Entity<Tomador>().OwnsMany(x => x.Dividas).HasData
                (
                    new Divida() { DividaId = 1, Valor = 1000M, DataAtualizacao = new DateTime(2020, 05, 30), TomadorId = 1 },
                    new Divida() { DividaId = 2, Valor = 1500M, DataAtualizacao = new DateTime(2020, 01, 15), TomadorId = 2 },
                    new Divida() { DividaId = 3, Valor = 2500M, DataAtualizacao = new DateTime(2020, 01, 03), TomadorId = 4 },
                    new Divida() { DividaId = 4, Valor = 2500M, DataAtualizacao = new DateTime(2020, 01, 03), TomadorId = 3 }
                );

            modelBuilder.Entity<Tomador>().OwnsMany(x => x.Dividas).OwnsMany(x => x.Simulacoes).HasData
                (
                    new Simulacao() { SimulacaoId = 1, DividaId = 1 },
                    new Simulacao() { SimulacaoId = 2, DividaId = 2 },
                    new Simulacao() { SimulacaoId = 3, DividaId = 3 },
                    new Simulacao() { SimulacaoId = 4, DividaId = 4 }
                );

            modelBuilder.Entity<Tomador>().OwnsMany(x => x.Dividas).OwnsMany(x => x.Simulacoes).OwnsMany(x => x.Acordos).HasData
                (
                    new Acordo() { AcordoId = 1, Ativo = true, SimulacaoId = 1 },
                    new Acordo() { AcordoId = 2, Ativo = true, SimulacaoId = 2 },
                    new Acordo() { AcordoId = 3, Ativo = true, SimulacaoId = 3 },
                    new Acordo() { AcordoId = 4, Ativo = true, SimulacaoId = 4 }
                );

            modelBuilder.Entity<Tomador>().OwnsMany(x => x.Dividas).OwnsMany(x => x.Simulacoes).OwnsMany(x => x.Parcelas).HasData
                (
                    new Parcela() { ParcelaId = 1, NumeroParcela = 1, ValorParcela = 100M, VencimentoParcela = new DateTime(2020, 09, 30), SimulacaoId = 1 },
                    new Parcela() { ParcelaId = 2, NumeroParcela = 1, ValorParcela = 150M, VencimentoParcela = new DateTime(2020, 10, 31), SimulacaoId = 2 },
                    new Parcela() { ParcelaId = 3, NumeroParcela = 1, ValorParcela = 170M, VencimentoParcela = new DateTime(2020, 12, 31), SimulacaoId = 3 },
                    new Parcela() { ParcelaId = 4, NumeroParcela = 1, ValorParcela = 170M, VencimentoParcela = new DateTime(2020, 12, 31), SimulacaoId = 4 }
                );
        }

    }
}
