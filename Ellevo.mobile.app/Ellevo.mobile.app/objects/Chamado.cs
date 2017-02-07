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
        public string UsuarioResponsavel { get; set; }
        /// <summary>
        /// Texto do chamado.
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Tipo do texto do chamado (.htm/.txt).
        /// </summary>
        public string TipoDescricao { get; set; }
        ///<summary>
        /// Indica se o chamado foi lido
        ///</summary>
        public bool Lido { get; set; }
    }
}
