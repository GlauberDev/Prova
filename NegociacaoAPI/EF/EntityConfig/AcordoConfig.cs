using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NegociacaoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.EF.EntityConfig
{
    public class AcordoConfig : IEntityTypeConfiguration<Acordo>
    {
        public void Configure(EntityTypeBuilder<Acordo> builder)
        {
            builder.HasKey(x => x.AcordoId);

            builder.Property(x => x.Ativo)
                .IsRequired();
        }
    }
}
