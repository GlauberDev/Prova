using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    public class NegociacaoCanceladaViewModel
    {
        public string msgStatus { get; set; }

        public NegociacaoCanceladaViewModel(string msgStatus)
        {
            this.msgStatus = msgStatus;
        }

    }
}
