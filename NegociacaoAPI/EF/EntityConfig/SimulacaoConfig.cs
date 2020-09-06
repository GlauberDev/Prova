using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegociacaoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.EF.EntityConfig
{
    public class SimulacaoConfig : IEntityTypeConfiguration<Simulacao>
    {
        public void Configure(EntityTypeBuilder<Simulacao> builder)
        {
            builder.HasKey(x => x.SimulacaoId);

            builder.HasMany<Acordo>()
                .WithOne(x => x.Simulacao)
                .HasForeignKey(y => y.SimulacaoId);

            builder.HasMany<Parcela>()
                .WithOne(x => x.Simulacao)
                .HasForeignKey(y => y.SimulacaoId);
        }
    }
}
