using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MontagemHTML
{
    class Program
    {
        static List<Menu> Menus;
        static StringBuilder Html;

        static void Main(string[] args)
        {
            /*************** INICIALIZA os menus ***************/
            Menus = new List<Menu>();
            //Menus raiz
            Menus.Add(new Menu(GerarNovoIdMenu(), "Elementos Processuais", null));
            Menus.Add(new Menu(GerarNovoIdMenu(), "Elementos do Direito", null));
            //Elementos Processuais
            Menus.Add(new Menu(GerarNovoIdMenu(), "Processo", BuscarMenu("Elementos Processuais")));
            Menus.Add(new Menu(GerarNovoIdMenu(), "Nota de Expediente", BuscarMenu("Elementos Processuais")));
            Menus.Add(new Menu(GerarNovoIdMenu(), "Andamento", BuscarMenu("Elementos Processuais")));
            Menus.Add(new Menu(GerarNovoIdMenu(), "Fase", BuscarMenu("Elementos Processuais")));
            //Elementos do Direito
            Menus.Add(new Menu(GerarNovoIdMenu(), "Pessoa", BuscarMenu("Elementos do Direito")));
            Menus.Add(new Menu(GerarNovoIdMenu(), "TJ Comarca", BuscarMenu("Elementos do Direito")));
            //Elementos do Direito >> Pessoa
            Menus.Add(new Menu(GerarNovoIdMenu(), "Contratante", BuscarMenu("Pessoa")));
            Menus.Add(new Menu(GerarNovoIdMenu(), "Pessoa Física", BuscarMenu("Pessoa")));


            /*************** IMPRIME os menus ***************/
            Html = new StringBuilder();
            ImprimirMenus(null);

            Console.WriteLine(Html);

        }

        /// <summary>
        /// Imprime os menus recursivamente.
        /// </summary>
        /// <param name="pMenuPai">Menu pai, cujos filhos, caso existam, serão impressos nesta chamada. Atribuir "null" caso este menu pai seja o raiz (caso esta seja a primeira chamada).</param>
        static void ImprimirMenus(Menu pMenuPai)
        {
            string? lHref = null;
            string? lTagsUlRaiz = null;
            string? lUltimoLiRaiz = null;
            List<Menu> lMenusFilhos;


            if (pMenuPai == null)//Se o menu pai for o raiz
            {
                lMenusFilhos = Menus.Where(x => x.ID_MENU_PAI == 0).ToList();
                lHref = "javascript:"; //Imprimir isto na tag <a> em vez do link para a página ASP
                lTagsUlRaiz = " id = \"treemenu1\" class=\"treeview\""; //Imprimir isto como atributos da <ul>
                lUltimoLiRaiz = "<li>" +
                    "\r\n<a href=\"javascript: sair()\">Sair</a>" +
                    "\r\n</li>" +
                    "\r\n";//Imprimir o link Sair no final (apenas)
            }
            //Senão, apenas a variável lMenusFilhos é carregada
            else
            {
                lMenusFilhos = Menus.Where(x => x.ID_MENU_PAI == pMenuPai.ID_MENU).ToList();
            }

            //pMenuAImprimir já teve seu <a> impresso (caso não seja o raiz)
            if (lMenusFilhos.Count() > 0)//Apenas se pMenuAImprimir tiver filhos:
            {
                Html.AppendLine(string.Format("<ul{0}>", lTagsUlRaiz ?? ""));//<ul> é aberta
                foreach (Menu fFilho in lMenusFilhos)//para cada filho:
                {
                    Html.AppendLine("<li>");
                    Html.AppendLine(string.Format("<a href=\"{0}\">{1}</a>", lHref ?? string.Format("{0}.aspx", TratarNomeAspx(fFilho.DESCRICAO_MENU)), fFilho.DESCRICAO_MENU));
                    ImprimirMenus(fFilho);//Chamada recursiva deste método, passando cada um dos filhos como parâmetro
                    Html.AppendLine("</li>");
                }
                Html.AppendLine(string.Format("{0}</ul>", lUltimoLiRaiz));

            }

        }

        /// <summary>
        /// Gera um número de ID disponível com base na lista global de menus
        /// </summary>
        static int GerarNovoIdMenu()
        {
            var lMenu = Menus.OrderBy(x => x.ID_MENU).LastOrDefault();//Maior ID atualmente usado
            if (lMenu != null)
                return lMenu.ID_MENU + 1;
            else
                return 1;
        }

        static Menu BuscarMenu(string pDescricao)
        {
            return Menus.FirstOrDefault(x => x.DESCRICAO_MENU == pDescricao);
        }

        /// <summary>
        /// Trata a string recebida para que seja usada como nome de página ASP fictício, removendo acentos e espaços
        /// </summary>
        static string TratarNomeAspx(string pNome)
        {
            pNome = pNome.Replace(" ", "");
            pNome = new string(pNome.Normalize(NormalizationForm.FormD).Where(x => char.GetUnicodeCategory(x) != UnicodeCategory.NonSpacingMark).ToArray());
            return pNome;
        }
    }

    class Menu
    {
        public Menu(int pId, string pDescricao, Menu pMenuPai)
        {

            ID_MENU = pId;
            DESCRICAO_MENU = pDescricao;
            ID_MENU_PAI = pMenuPai != null ? pMenuPai.ID_MENU : 0;

        }

        public int ID_MENU { get; set; }
        public string DESCRICAO_MENU { get; set; }
        public int ID_MENU_PAI { get; set; }
    }
}
