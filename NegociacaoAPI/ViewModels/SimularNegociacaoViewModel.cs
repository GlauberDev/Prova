using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para simular negociacao de dívida via API 
    /// </summary>
    public class SimularNegociacaoViewModel
    {
        [DisplayName("CPF")]
        public string cpf { get; set; }
        [DisplayName("Qtd. Parcelas")]
        public int qtdParcelas { get; set; }
    }
}
