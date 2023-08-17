using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    public class Funcionario
    {

        public long Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Genero { get; set; }

        public long? EnderecoID { get; set; }

        public string? CNH { get; set; }

        public string? Cargo { get; set; }

        public string ToString()
        {
            return $"{Id};{Nome};{DataNascimento};{Cpf};{Rg};{Email};{Telefone};{Genero};{EnderecoID};{CNH};{Cargo};";
        }
    }
}
