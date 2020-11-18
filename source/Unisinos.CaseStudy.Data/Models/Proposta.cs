using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisinos.CaseStudy.Data.Models
{
    public class Proposta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropostaId { get; set; }

        public int ClienteId { get; set; }

        public PropostaStatus Status { get; set; }

        public DateTime DataCriacao { get; set; }

        public double Valor { get; set; }

        public int UsuarioId { get; set; }

        public virtual IList<Documento> Documentos { get; set; }

        public virtual IList<PropostaHistorico> Historico { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        // Criador da Proposta, pode ser um Vendedor ou Correspondente
        [ForeignKey("UsuarioId")]
        public virtual Usuario Responsavel { get; set; }

    }
}
