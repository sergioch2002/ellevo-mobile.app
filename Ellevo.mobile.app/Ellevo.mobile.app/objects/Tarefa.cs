using System;

namespace Ellevo.mobile.app
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public string Titulo { get; set; }
        public int QuantidadeProvidencias { get; set; }
        public int QuantidadeInstrucoes { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? Vencimento { get; set; }
        public int Status { get; set; }
        public int UsuarioResponsavelID { get; set; }
        public string UsuarioResponsavel { get; set; }
        public string Descricao { get; set; }
        public string TipoDescricao { get; set; }
        /// <summary>
        /// Campo para indicar se a tarefa já foi lida
        /// </summary>
        public bool Lido { get; set; }
    }
}
