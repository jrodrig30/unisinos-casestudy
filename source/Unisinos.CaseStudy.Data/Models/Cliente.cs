using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisinos.CaseStudy.Data.Models
{
    public class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public string CPF { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Sexo { get; set; }

        public DateTime DataNascimento { get; set; }
        
        public int EnderecoId { get; set; }

        public virtual Endereco Endereco { get; set; }

        public int Idade
        {
            get
            {
                return DateTime.Now.Year - this.DataNascimento.Year;
            }
        }
    }
}
