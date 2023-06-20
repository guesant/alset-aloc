using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerramentasBiblioteca.Formatacoes;

namespace FerramentasBiblioteca.Validacoes
{
    public static class ValidacoesSociais
    {
        public static bool ValidarCpf(string cpf)
        {
            bool verificado;
            int primeiroDigitoVerificador, primeiroDigitoVerificadorCalculado, segundoDigitoVerificador, segundoDigitoVerificadorCalculado, total = 0, peso = 10;

            cpf = Mascaras.ManterApenasNumeros(cpf).PadLeft(11, '0').Substring(0, 11);
            primeiroDigitoVerificador = Convert.ToInt32(cpf.Substring(9, 1));
            segundoDigitoVerificador = Convert.ToInt32(cpf.Substring(10, 1));

            for (int i = 0; i < 9; i++)
            {
                total += Convert.ToInt32(cpf.Substring(i, 1)) * peso;
                peso--;
            }

            primeiroDigitoVerificadorCalculado = 11 - (total % 11);

            if(primeiroDigitoVerificadorCalculado >= 10)
            {
                primeiroDigitoVerificadorCalculado = 0;
            }

            total = 0;
            peso = 11;

            for (int i = 0; i < 10; i++)
            {
                int digitoAtual;
                
                if(i < 9)
                {
                    digitoAtual = Convert.ToInt32(cpf.Substring(i, 1));
                    
                } else
                {
                    digitoAtual = primeiroDigitoVerificador;
                }
                
                total += digitoAtual * peso;
            }

            segundoDigitoVerificadorCalculado = 11 - (total % 11);

            if(segundoDigitoVerificadorCalculado >= 10)
            {
                segundoDigitoVerificadorCalculado = 0;
            }

            verificado = (primeiroDigitoVerificador == primeiroDigitoVerificadorCalculado) && (segundoDigitoVerificador == segundoDigitoVerificadorCalculado);

            if (!verificado)
            {
                Console.Error.WriteLine("Inválido!");
            }

            return verificado;
        }

        public static bool ValidarCnpj(string cnpj)
        {
            bool verificado;
            string partePrincipalRevertida;
            int primeiroDigitoVerificador, primeiroDigitoVerificadorCalculado, segundoDigitoVerificador, segundoDigitoVerificadorCalculado, total = 0, peso;

            cnpj = Mascaras.ManterApenasNumeros(cnpj).PadLeft(14, '0');
            primeiroDigitoVerificador = Convert.ToInt32(cnpj.Substring(12, 1));
            segundoDigitoVerificador = Convert.ToInt32(cnpj.Substring(13, 1));

            partePrincipalRevertida = new string(cnpj.Substring(0, 12).Reverse().ToArray());

            for (int i = 0; i < 12; i++)
            {
                peso = (i % 8) + 2;
                total += Convert.ToInt32(partePrincipalRevertida.Substring(i, 1)) * peso;
            }

            primeiroDigitoVerificadorCalculado = 11 - (total % 11);

            if(primeiroDigitoVerificadorCalculado >= 10)
            {
                primeiroDigitoVerificadorCalculado = 0;
            }

            // vamos adicionar o primeiro digito verificador calculado ao ínicio da parte principal
            // e repetir o processo para achar o segundo digito verificador

            partePrincipalRevertida = primeiroDigitoVerificadorCalculado.ToString() + partePrincipalRevertida;

            total = 0;

            for (int i = 0; i < 13; i++)
            {
                peso = (i % 8) + 2;
                int digitoAtual = Convert.ToInt32(partePrincipalRevertida.Substring(i, 1));
                total += digitoAtual * peso;
            }

            segundoDigitoVerificadorCalculado = 11 - (total % 11);

            if (segundoDigitoVerificadorCalculado >= 10)
            {
                segundoDigitoVerificadorCalculado = 0;
            }

            verificado = (primeiroDigitoVerificador == primeiroDigitoVerificadorCalculado) && (segundoDigitoVerificador == segundoDigitoVerificadorCalculado);

            if (!verificado)
            {
                Console.Error.WriteLine("Inválido!");
            }

            return verificado;
        }
    }
}
