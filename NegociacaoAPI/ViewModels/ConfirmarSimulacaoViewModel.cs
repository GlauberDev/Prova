using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para confirmar negociação via API 
    /// </summary>
    public class ConfirmarSimulacaoViewModel
    {
        [DisplayName("CPF")]
        public string cpf { get; set; }
        public int simulacaoId { get; set; }
        public ConfirmarSimulacaoViewModel()
        {

        }
        public ConfirmarSimulacaoViewModel(string cpf, int simulacaoId)
        {
            this.cpf = cpf;
            this.simulacaoId = simulacaoId;
        }
    }
}
