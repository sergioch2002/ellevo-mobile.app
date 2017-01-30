using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.objects
{
    public class Conta
    {
        public int ContaId { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int QuantidadeInstrucoes { get; set; }
        public bool Lido { get; set; }
    }
}
