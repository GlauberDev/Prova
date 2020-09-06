using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.Models
{
    public class Simulacao
    {
        [Key]
        public int SimulacaoId { get; set; }
        public List<Acordo> Acordos { get; set; }
        public List<Parcela> Parcelas { get; set; }
        public int DividaId { get; set; }
        public Divida Divida { get; set; }

        public Simulacao()
        {
            Acordos = new List<Acordo>();
            Parcelas = new List<Parcela>();
        }
    }
}
