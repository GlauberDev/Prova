using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NegociacaoAPI.ViewModels
{
    /// <summary>
    /// ViewModel para receber lista de CPFs de tomadores via API 
    /// </summary>
    public class CPFsListadosViewModel
    {
        [DisplayName("CPF")]
        public string cpf { get; set; }
    }
}
