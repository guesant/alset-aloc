using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;

namespace alset_aloc.Helpers
{
    internal class Mascaras
    {

        public static string LimparCPF(string cpfOriginal)
        {
            var padrao = new Regex("\\D");
            var cpfLimpo = padrao.Replace(cpfOriginal, "");
            return cpfLimpo;
        }

        public static string FormatarCPF(string cpfOriginal)
        {
            var cpf = LimparCPF(cpfOriginal);
            var cpfFormatado = $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            return cpfFormatado;
        }

        public static string LimparTelefone(string telefoneOriginal)
        {
            var telefoneLimpo = telefoneOriginal;
            return telefoneLimpo;
        }

        public static string FormatarTelefone(string telefoneOriginal)
        {
            var telefone = LimparCPF(telefoneOriginal);
            return telefone;
        }

    }
}
