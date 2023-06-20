using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerramentasBiblioteca.Calculos
{
    public static class MDatas
    {
        public static int CalcularIdade(string dataNasc)
        {
            //   00/00/0000
            int dia = Convert.ToInt32(dataNasc.Substring(0, 2));
            int mes = Convert.ToInt32(dataNasc.Substring(3, 2));
            int ano = Convert.ToInt32(dataNasc.Substring(6, 4));

            DateTime dataN = new DateTime(ano, mes, dia);
            DateTime dataHoje = DateTime.Today;

            TimeSpan diferenca = dataHoje - dataN;

            int idade = (int)((diferenca.Days + 1) / 365.2425);

            return idade;
        }

        public static string CalcularTempoDeVida(string dataNasc)
        {
            int dia = Convert.ToInt32(dataNasc.Substring(0, 2));
            int mes = Convert.ToInt32(dataNasc.Substring(3, 2));
            int ano = Convert.ToInt32(dataNasc.Substring(6, 4));

            DateTime dataN = new DateTime(ano, mes, dia);
            DateTime dataHoje = DateTime.Now;

            TimeSpan diferenca = dataHoje - dataN;

            int idade = (int)((diferenca.Days + 1) / 365.2425);
            int meses = (int)((diferenca.Days + 1) / 30.436875);
            int dias = diferenca.Days - (int)(meses * 30.436875);

            string msg = idade + " ano(s), " + (meses - (idade * 12)) + " meses e " + dias + " dia(s)";
            return msg;
        }


        public static DateTime ConverterParaDateTime(string data)
        {
            int dia = Convert.ToInt32(data.Substring(0, 2));
            int mes = Convert.ToInt32(data.Substring(3, 2));
            int ano = Convert.ToInt32(data.Substring(6, 4));

            DateTime dataN = new DateTime(ano, mes, dia);

            return dataN;
        }


        public static int DifDataInicialEDataFinalEmAnos(DateTime dataInicial, DateTime dataFinal)
        {
            TimeSpan diferença = dataFinal - dataInicial;

            int anos = (int)(diferença.Days / 365.2425);

            return anos;
        }

        public static int DifDataInicialEDataFinalEmMeses(DateTime dataInicial, DateTime dataFinal)
        {
            TimeSpan diferença = dataFinal - dataInicial;

            int anos = (int)(diferença.Days / 30.436875);

            return anos;
        }


        public static int DifDataInicialEDataDeHojeEmAnos(string dataInical)
        {
            return DifDataInicialEDataFinalEmAnos(ConverterParaDateTime(dataInical), DateTime.Today);

        }
        
        public static int DifDataInicialEDataDeHojeEmAnos(DateTime dataInical)
        {
            return DifDataInicialEDataFinalEmAnos(dataInical, DateTime.Today);
        }

        public static int DifDataInicialEDataDeHojeEmMeses(string dataInical)
        {
            return DifDataInicialEDataFinalEmMeses(ConverterParaDateTime(dataInical), DateTime.Today);

        }
        public static int DifDataInicialEDataDeHojeEmMeses(DateTime dataInical)
        {
            return DifDataInicialEDataFinalEmMeses(dataInical, DateTime.Today);
        }

        public static DateTime SomarDiasParaUmaData(DateTime dataN, int diasFinal)
        {
            DateTime soma = dataN.AddDays(diasFinal);
            return soma;
        }
        
        public static DateTime SomarDiasParaUmaData(DateTime dataN, string dias)
        {
            int diasFinal = Convert.ToInt32(dias);
            return SomarDiasParaUmaData(dataN, diasFinal);
        }

        public static DateTime SomarDiasParaUmaData(string data, int dias)
        {
            DateTime dataN = ConverterParaDateTime(data);
            return SomarDiasParaUmaData(dataN, dias);
        }
        
        public static DateTime SomarDiasParaUmaData(string data, string dias)
        {
            int diasFinal = Convert.ToInt32(dias);
            return SomarDiasParaUmaData(data, diasFinal);
        }
        
        public static DateTime SomarDiasAPartirDataAtual(int dia)
        {
            return SomarDiasParaUmaData(DateTime.Today, dia);

        }
        
        public static DateTime SomarDiasAPartirDataAtual(string dias)
        {
            int diafianal = Convert.ToInt32(dias);

            return SomarDiasAPartirDataAtual(diafianal);

        }
    }
}
