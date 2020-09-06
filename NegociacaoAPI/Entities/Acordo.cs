using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.Models
{
    public class Acordo
    {
        [Key]
        public int AcordoId { get; set; }
        public bool Ativo { get; set; }
        public int SimulacaoId { get; set; }
        public Simulacao Simulacao { get; set; }
    }
}
