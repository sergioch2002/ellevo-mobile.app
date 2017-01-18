using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.objects
{
    public class Configuracoes
    {
        public List<DominioMobile> ListaDominios { get; set; }
        public string NomeSite { get; set; }
        public bool UsaDominio { get; set; }
        public int DominioPadraoId { get; set; }
        public List<IdiomaMobile> ListaIdiomas { get; set; }
    }
}
