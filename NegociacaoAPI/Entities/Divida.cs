using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.Models
{
    public class Divida
    {
        [Key]
        public int DividaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<Simulacao> Simulacoes { get; set; }
        public int TomadorId { get; set; }
        public Tomador Tomador { get; set; }
    }
}
