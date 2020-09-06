

using System;
using System.ComponentModel;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para consultar dívida do tomador via API 
    /// </summary>
    public class ConsultarDividaViewModel
    {
        [DisplayName("CPF")]
        public string cpf { get; set; }
        [DisplayName("Dívida")]
        public decimal divida { get; set; }
        [DisplayName("Data de atualização")]
        public DateTime dataAtualizacao { get; set; }

        public ConsultarDividaViewModel(string cpf, decimal divida, DateTime dataAtualizacao)
        {
            this.cpf = cpf;
            this.divida = divida;
            this.dataAtualizacao = dataAtualizacao;
        }

    }
}
