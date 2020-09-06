using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.EF.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<NegociacaoContext>
    {
        public NegociacaoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NegociacaoContext>();
            optionsBuilder.UseSqlite("Data Source=negociacao.db");

            return new NegociacaoContext(optionsBuilder.Options);
        }
    }
}
