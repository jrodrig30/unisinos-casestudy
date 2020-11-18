using System;
using System.Collections.Generic;
using System.Text;

namespace Unisinos.CaseStudy.Business.Helpers
{
    public static class CpfValidator
    {
        public static bool Validate(string cpf)
        {
            var multipliers = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multipliers2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string digito;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            var firstPart = cpf.Substring(0, 9);

            var soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(firstPart[i].ToString()) * multipliers[i];

            var resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            firstPart = firstPart + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(firstPart[i].ToString()) * multipliers2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool CheckIrregular(string cpf)
        {
            return true;
        }
    }
}
