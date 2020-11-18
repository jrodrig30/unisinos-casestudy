using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Unisinos.CaseStudy.Shared.Requests
{
    [DataContract]
    public class PropostaRequest
    {
        [DataMember]
        [Required]
        public int ClienteId { get; set; }

        [DataMember]
        [Required]
        public double Valor { get; set; }

        [DataMember]
        [Required]
        public int ResponsavelId { get; set; }
    }
}
