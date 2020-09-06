using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para receber retorno de cancelamento de negociação via API 
    /// </summary>
    public class CancelarNegociacaoViewModel
    {
        [DisplayName("CPF")]
        public string cpf { get; set; }
        public int acordoId { get; set; }

        public CancelarNegociacaoViewModel()
        {

        }

        public CancelarNegociacaoViewModel(string cpf, int acordoId)
        {
            this.cpf = cpf;
            this.acordoId = acordoId;
        }
    }
}
