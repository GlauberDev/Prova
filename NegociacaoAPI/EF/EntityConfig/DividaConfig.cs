using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegociacaoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.EF.EntityConfig
{
    public class DividaConfig : IEntityTypeConfiguration<Divida>
    {
        public void Configure(EntityTypeBuilder<Divida> builder)
        {
            builder.HasKey(x => x.DividaId);

            builder.Property(x => x.Valor)
                .IsRequired();

            builder.Property(x => x.DataAtualizacao)
                .IsRequired();

            builder.HasMany<Simulacao>()
                .WithOne(x => x.Divida)
                .HasForeignKey(y => y.DividaId);

        }
    }
}
