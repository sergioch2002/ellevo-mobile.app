using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app
{
    public class Filtro
    {
        public string OrdenarPor { get; set; }
        public EPaginacaoOrdem? OrdemTipo { get; set; }
        public int? Pagina { get; set; }
        public int? PaginaInicio { get; set; }
        public int? PaginaTamanho { get; set; }
        public string Pesquisa { get; set; }
    }
}
