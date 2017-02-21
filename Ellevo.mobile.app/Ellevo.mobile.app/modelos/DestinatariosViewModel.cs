using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.modelos
{
    public class DestinatariosViewModel
    {
        public ObservableCollection<Grouping<InstrucaoDestinatario, InstrucaoDestinatario>> InstrucaoDestinatarioAgrupado { get; set; }
        public string Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Nome) || Nome.Length == 0)
                    return "?";

                return Nome;
            }
        }
    }
}
