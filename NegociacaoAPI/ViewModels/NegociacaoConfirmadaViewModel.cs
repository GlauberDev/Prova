using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para o retorno da confirmação de negociação via API 
    /// </summary>
    public class NegociacaoConfirmadaViewModel
    {
        public int acordoId { get; set; }
        public string msgStatus { get; set; }

        public NegociacaoConfirmadaViewModel(int idAcordo, string msgStatus)
        {
            this.acordoId = idAcordo;
            this.msgStatus = msgStatus;
        }

    }
}
