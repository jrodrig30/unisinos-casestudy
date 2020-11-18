using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisinos.CaseStudy.Data.Models
{
    public class PropostaHistorico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropostaHistoricoId { get; set; }

        [ForeignKey("Proposta")]
        public int PropostaId { get; set; }

        public PropostaStatus Status { get; set; }

        public DateTime Data { get; set; }
    }
}
