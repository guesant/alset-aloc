using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerramentasBiblioteca.Formatacoes;

namespace FerramentasBiblioteca.Validacoes
{
    public static class ContaBancaria
    {
        public static bool ValidarContaBB(string agencia, string conta)
        {
            int total = 0, digitoVerificador, digitoVerificadorCalculado;
            
            conta = conta.PadLeft(9, '0').Substring(0, 9).ToUpper();

            string ultimoDigito = conta.Substring(8, 1);

            digitoVerificador = ultimoDigito == "X" ? 10 : Convert.ToInt32(ultimoDigito);

            for (int i = 0; i < 8; i++)
            {
                int peso = 9 - i;
                total += Convert.ToInt32(conta[i]) * peso;
            }

            digitoVerificadorCalculado = 11 - (total % 11);

            if(digitoVerificadorCalculado == 11) {
                digitoVerificadorCalculado = 0;
            }

            if(digitoVerificador != digitoVerificadorCalculado)
            {
                Console.Error.WriteLine("Inválido!");
                return false;
            }

            return true;
        }

        public static bool ValidarContaCitiBank(string agencia, string conta)
        {
            bool verificado;
            int digitoVerificador, digitoVerificadorCalculado, restoPor11, total = 0, peso = 11;
            
            conta = Mascaras.ManterApenasNumeros(conta).PadLeft(11, '0').Substring(0, 11);
            digitoVerificador = Convert.ToInt32(conta.Substring(10, 1));

            for(int i = 0; i < 10; i++)
            {
                int digitoAtual = Convert.ToInt32(conta.Substring(i, 1));
                total += digitoAtual * peso;
                peso--;
            }
            
            restoPor11 = (total % 11);

            if(restoPor11 > 1)
            {
                digitoVerificadorCalculado = 11 - restoPor11;
            } else
            {
                digitoVerificadorCalculado = 0;
            }

            verificado = digitoVerificadorCalculado == digitoVerificador;

            if(!verificado)
            {
                Console.Error.WriteLine("Inválido!");
                return false;
            }

            return true;
        }

        public static bool ValidarContaCEF(string agencia, string conta)
        {
            bool verificado;
            string blocoAtual;
            int digitoVerificador, digitoVerificadorCalculado, peso, total = 0;
            
            agencia = Mascaras.ManterApenasNumeros(agencia).PadLeft(4, '0').Substring(0, 4);
            conta = Mascaras.ManterApenasNumeros(conta).PadLeft(12, '0').Substring(0, 12);

            
            digitoVerificador = Convert.ToInt32(conta.Substring(11, 1));
        
            peso = 8;
            blocoAtual = agencia + conta.Substring(0, 3);

            for(int i = 0; i < 7; i++)
            {
                total += Convert.ToInt32(blocoAtual.Substring(i, 1)) * peso;
                peso--;
            }

            peso = 9;
            blocoAtual = conta.Substring(3, 8);

            for (int i = 0; i < 8; i++)
            {
                total += Convert.ToInt32(blocoAtual.Substring(i, 1)) * peso;
                peso--;
            }

            total *= 10;

            digitoVerificadorCalculado = total % 11;

            if(digitoVerificadorCalculado == 10) {
                digitoVerificadorCalculado = 0;
            }

            verificado = digitoVerificador == digitoVerificadorCalculado;

            if (!verificado)
            {
                Console.Error.WriteLine("Inválido!");
            }

            return verificado;
        }
    
        public static bool ValidarContaItau(string agencia, string conta)
        {
            string blocoTotal;
            bool verificado;
            int digitoVerificadorCalculado, digitoVerificador, valorAtual, digitoAtual, resto, peso = 2, total = 0;

            agencia = Mascaras.ManterApenasNumeros(agencia).PadLeft(4, '0').Substring(0, 4);
            conta = Mascaras.ManterApenasNumeros(conta).PadLeft(6, '0').Substring(0, 6);
            
            digitoVerificador = Convert.ToInt32(conta.Substring(5, 1));
            
            blocoTotal = agencia + conta;

            for (int i = 0; i < 9; i++)
            {
                digitoAtual = Convert.ToInt32(blocoTotal.Substring(i, 1));

                valorAtual = digitoAtual * peso;

                if(valorAtual > 9)
                {
                    int dezena = valorAtual / 10;
                    int unidade = valorAtual % 10;
                    
                    valorAtual = dezena + unidade;
                }
                
                total += valorAtual;

                if(peso == 2)
                {
                    peso = 1;
                }
                else
                {
                    peso = 2;
                }
            }

            resto = (total % 10);

            if(resto == 0)
            {
                digitoVerificadorCalculado = 0;
            }
            else
            {
                digitoVerificadorCalculado = 10 - resto;
            }

            verificado = digitoVerificadorCalculado == digitoVerificador;

            if(!verificado)
            {
                Console.Error.WriteLine("Inválido!");
            }

            return verificado;
        }    
    }
}
