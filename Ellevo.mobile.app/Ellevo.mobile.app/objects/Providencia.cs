using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app
{
    public class Providencia
    {
        public int? ProvidenciaId { get; set; }
        public int TarefaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
        public string Descricao { get; set; }
        public string TipoDescricao { get; set; }
        public bool ProvidenciaSolucao { get; set; }
    }
}
