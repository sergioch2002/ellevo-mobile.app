using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.objects
{
    public class Instrucao
    {
        public int InstrucaoId { get; set; }

        public string Titulo { get; set; }

        public string Remetente { get; set; }

        public bool? Lida { get; set; }

        public DateTime? DataCadastro { get; set; }

        public string Origem { get; set; }

        public int? OrigemId { get; set; }

        public int OrigemNumero { get; set; }

        public int OrigemExpressao { get; set; }
    }
}
