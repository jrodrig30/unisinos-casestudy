using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Unisinos.CaseStudy.Data.Models.Enums;

namespace Unisinos.CaseStudy.Data.Models
{
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public Perfil Perfil { get; set; }

        public DateTime UltimoAcesso { get; set; }
    }
}
