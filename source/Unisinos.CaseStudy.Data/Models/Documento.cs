using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisinos.CaseStudy.Data.Models
{
    public class Documento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentoId { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        public TipoDocumento TipoDocumento { get; set; }

        public byte[] Arquivo { get; set; }

        [ForeignKey("Proposta")]
        public int PropostaId { get; set; }
    }

    public enum TipoDocumento
    {
        RG = 1,
        CarteiraMotorista = 2,
        CertidaoNascimento = 3,
        ComprovanteEndereco = 4,
        ComprovanteRenda = 5
    }
}
