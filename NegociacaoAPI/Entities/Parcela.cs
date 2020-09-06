using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.Models
{
    public class Parcela
    {
        [Key]
        public int ParcelaId { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime VencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public int SimulacaoId { get; set; }
        public Simulacao Simulacao { get; set; }
    }
}
