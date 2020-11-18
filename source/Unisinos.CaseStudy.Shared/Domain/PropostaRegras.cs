using System;
using System.Collections.Generic;
using System.Text;
using Unisinos.CaseStudy.Shared.Exceptions;

namespace Unisinos.CaseStudy.Shared.Domain
{
    public class PropostaRegras
    {
        private const double VALOR_MINIMO = 300;
        private const double VALOR_MAXIMO = 60000;

        private const int IDADE_MINIMA = 18;
        private const int IDADE_MAXIMA = 75;

        public bool ValorDentroDosLimites(double valor)
        {
            if(valor > VALOR_MAXIMO)
            {
                throw new BusinessException("ValorMaximo", $"Valor fora dos limites: O valor está {VALOR_MAXIMO - valor} acima do permitido.");
            }

            if (valor < VALOR_MINIMO)
            {
                throw new BusinessException("ValorMinimo", $"Valor fora dos limites: O valor está {VALOR_MINIMO - valor} abaixo do permitido.");
            }

            return true;
        }

        public bool IdadeDentroDosLimites(int idade)
        {
            if (idade > IDADE_MAXIMA)
            {
                throw new BusinessException("IdadeMaxima", $"Idade fora dos limites: A idade máxima é {IDADE_MAXIMA}.");
            }

            if (idade < IDADE_MINIMA)
            {
                throw new BusinessException("IdadeMinima", $"Idade fora dos limites: A idade mínima é {IDADE_MINIMA}.");
            }

            return true;
        }

        public bool QuantidadePropostasAcimaPermitido(int valor)
        { 
            if(valor >= 5)
                throw new BusinessException("ExcedeuNumeroPropostas", "Este CPF já atingiu o limite máximo de propostas.");

            return true;
        }
    }
}
