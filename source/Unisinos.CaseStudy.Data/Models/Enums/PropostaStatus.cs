using System;
using System.Collections.Generic;
using System.Text;

namespace Unisinos.CaseStudy.Data.Models
{
    public enum PropostaStatus
    {
        Criada = 0,

        AguardandoValidacao = 10,

        PendenciaAutomatica = 20,

        Reprovada = 21,

        PendenciaDocumentacao = 30,

        EnviadaConvenio = 50,

        ReprovadaConvenio = 51,

        AguardandoPagamento = 60,

        Aprovada = 100,

        Cancelada = 101
    }
}
