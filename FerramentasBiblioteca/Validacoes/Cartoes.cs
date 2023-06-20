using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerramentasBiblioteca.Formatacoes;

namespace FerramentasBiblioteca.Validacoes
{
    public static class Cartoes
    {


        public  static bool ValidarCartaoDeCredito( string numeroComVerificador)
        {

            /*
             * Fontes:
             * - https://en.wikipedia.org/wiki/Luhn_algorithm
             */

            bool verificado;
            int digitoVerificador, digitoVerificadorCalculado, total = 0;
            string numero, numeroInvertido;

            numeroComVerificador = Mascaras.ManterApenasNumeros(numeroComVerificador);
            
            digitoVerificador = Convert.ToInt32(numeroComVerificador.Substring(numeroComVerificador.Length - 1 , 1));

            numero = numeroComVerificador.Substring(0, numeroComVerificador.Length - 1 );

            numeroInvertido = new string(numero.Reverse().ToArray());

            Console.WriteLine($"Número Com Verificador: {numeroComVerificador}");
            Console.WriteLine($"    Digito Verificador: {digitoVerificador}");
            Console.WriteLine($"Número Sem Verificador: {numero}");
            Console.WriteLine($"      Número Invertido: {numeroInvertido}");

            Console.WriteLine("==============================================================");

            for (int i = 0; i < numeroInvertido.Length; i++)
            {
                int digitoAtual, multiplicador, valorASerSomado;

                digitoAtual = Convert.ToInt32(numeroInvertido.Substring(i, 1));

                if (i % 2 == 0)
                {
                    multiplicador = 2;
                }
                else
                {
                    multiplicador = 1;
                }

                valorASerSomado = digitoAtual * multiplicador;

                if (valorASerSomado > 9)
                {
                    valorASerSomado -= 9;
                }

                total += valorASerSomado;

                Console.WriteLine($"-> Digito Atual: {digitoAtual} | Multiplicador: {multiplicador} | Valor a ser somado: {valorASerSomado}");
            }

            Console.WriteLine("==============================================================");

            digitoVerificadorCalculado = 10 - (total % 10);
            
            Console.WriteLine($"Verificador Calculado: {digitoVerificadorCalculado}");

            verificado = digitoVerificadorCalculado == digitoVerificador;

            if(!verificado)
            {
                Console.Error.WriteLine("Inválido!");
            }

            return verificado;
        }


    }
}
