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

            //Retorna o HTML
            Console.WriteLine(Html);

        }

        static void ImprimirMenus(Menu pMenuPai)
        {
            string? lHref = null;
            string? lTagsUlRaiz = null;
            string? lUltimoLiRaiz = null;
            List<Menu> lMenusFilhos;


            if (pMenuPai == null)//Se o menu pai for o raiz
            {
                lMenusFilhos = Menus.Where(x => x.ID_MENU_PAI == 0).ToList();
                lHref = "javascript:";
                lTagsUlRaiz = " id = \"treemenu1\" class=\"treeview\"";
                lUltimoLiRaiz = "<li>" +
                    "\r\n<a href=\"javascript: sair()\">Sair</a>" +
                    "\r\n</li>" +
                    "\r\n";
            }
            else
            {
                lMenusFilhos = Menus.Where(x => x.ID_MENU_PAI == pMenuPai.ID_MENU).ToList();
            }


            if (lMenusFilhos.Count() > 0)
            {
                Html.AppendLine(string.Format("<ul{0}>", lTagsUlRaiz ?? ""));
                foreach (Menu fFilho in lMenusFilhos)
                {
                    Html.AppendLine("<li>");
                    Html.AppendLine(string.Format("<a href=\"{0}\">{1}</a>", lHref ?? string.Format("{0}.aspx", TratarNomeAspx(fFilho.DESCRICAO_MENU)), fFilho.DESCRICAO_MENU));
                    ImprimirMenus(fFilho);
                    Html.AppendLine("</li>");
                }
                Html.AppendLine(string.Format("{0}</ul>", lUltimoLiRaiz));

            }

        }

        static int GerarNovoIdMenu()
        {
            var lMenu = Menus.OrderBy(x => x.ID_MENU).LastOrDefault();
            if (lMenu != null)
                return lMenu.ID_MENU + 1;
            else
                return 1;
        }

        static Menu BuscarMenu(string pDescricao)
        {
            return Menus.FirstOrDefault(x => x.DESCRICAO_MENU == pDescricao);
        }

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
