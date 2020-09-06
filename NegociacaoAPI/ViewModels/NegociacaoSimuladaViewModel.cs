using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para o retorno da negociação simulada via API 
    /// </summary>
    public class NegociacaoSimuladaViewModel
    {
        [DisplayName("CPF")]
        public string cpf { get; set; }
        public int simulacaoId { get; set; }
        public ParcelamentoViewModel parcelamento { get; set; }

        public NegociacaoSimuladaViewModel(string cpf, int simulacaoId, ParcelamentoViewModel parcelamento)
        {
            this.cpf = cpf;
            this.simulacaoId = simulacaoId;
            this.parcelamento = parcelamento;
        }
    }
}
