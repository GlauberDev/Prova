using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegociacaoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.EF.EntityConfig
{
    public class ParcelamentoConfig : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.HasKey(x => x.ParcelaId);

            builder.Property(x => x.NumeroParcela)
                .IsRequired();

            builder.Property(x => x.VencimentoParcela)
                .IsRequired();

            builder.Property(x => x.ValorParcela)
                .IsRequired();
        }
    }
}
