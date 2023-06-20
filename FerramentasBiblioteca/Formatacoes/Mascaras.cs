using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FerramentasBiblioteca.Formatacoes
{
    public static class Mascaras
    {
        public static string ManterApenasNumeros(string entrada)
        {
            // \\D significa: "tudo o que não for digito" ... vai ser substituido por ""
            return new Regex("\\D").Replace(entrada, "");
        }

        public static string ColocarMascaraDoTelefone(string num)
        {
            num = ManterApenasNumeros(num);

            string ddd = num.Substring(0, 2);
            string num1 = num.Substring(2, 5);
            string num2 = num.Substring(7, 4);
            //colocar máscara

            string msk = "(" + ddd + ")-" + num1 + "-" + num2;
            return msk;
        }

        public static string ColocarMascaraDoTelefone(long num)
        {
            return ColocarMascaraDoTelefone(Convert.ToString(num));
        }

        public static string Mensagem(string m)
        {
            return m; 
        }
    }
}
