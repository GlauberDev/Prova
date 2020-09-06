using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para receber o parcelamento CONTIDO no retorno da negociação simulada via API 
    /// </summary>
    public class ParcelamentoViewModel
    {
        [DisplayName("Número da parcela")]
        public int numeroParcela { get; set; }

        [DisplayName("Vencimento")]
        public DateTime vencimentoParcela { get; set; }

        [DisplayName("Valor")]
        public decimal valorParcela { get; set; }

        public ParcelamentoViewModel(int numeroParcela, DateTime vencimentoParcela, decimal valorParcela)
        {
            this.numeroParcela = numeroParcela;
            this.vencimentoParcela = vencimentoParcela;
            this.valorParcela = valorParcela;
        }

    }
}
