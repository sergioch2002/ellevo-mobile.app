using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app
{
    public class Tramite
    {
        public int TramiteId { get; set; }
        public int ChamadoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
        public string Descricao { get; set; }
        public string TipoDescricao { get; set; }
        public bool TramiteSolucao { get; set; }
        public List<Status> Status { get; set; }
        public List<Motivo> Motivos { get; set; }
        public List<Atividade> Atividades { get; set; }
    }
}
