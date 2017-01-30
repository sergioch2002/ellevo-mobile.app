using System;

namespace Ellevo.mobile.app
{
    public class Chamado
    {
        public int ChamadoId { get; set; }
        public string Titulo { get; set; }
        public int QuantidadeTramites { get; set; }
        public int QuantidadeInstrucoes { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? Vencimento { get; set; }
        public int Status { get; set; }
        public int UsuarioResponsavelId { get; set; }
    }
}
