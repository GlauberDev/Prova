using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.Models
{
    public class Tomador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TomadorId { get; set; }
        public string CPF { get; set; }
        public List<Divida> Dividas { get; set; }
    }
}
