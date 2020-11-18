using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Unisinos.CaseStudy.Shared.Requests
{
    [DataContract]
    public class ClienteRequest
    {
        [DataMember]
        [Required]
        public string CPF { get; set; }

        [DataMember]
        [Required]
        public string Nome { get; set; }

        [DataMember]
        [Required]
        public string Email { get; set; }

        [DataMember]
        [Required]
        public string Sexo { get; set; }

        [DataMember]
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}