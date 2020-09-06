using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegociacaoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NegociacaoAPI.EF.EntityConfig
{
    public class TomadorConfig : IEntityTypeConfiguration<Tomador>
    {
        public void Configure(EntityTypeBuilder<Tomador> builder)
        {
            builder.HasKey(x => x.TomadorId);

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength();

            builder.HasMany<Divida>()
                .WithOne(x => x.Tomador)
                .HasForeignKey(y => y.TomadorId);
        }
    }
}
