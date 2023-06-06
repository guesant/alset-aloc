using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set;  }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string CNH { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Genero { get; set; }

        // public Endereco Endereco { get; set; }

        // public List<CompraLocacao> CompraLocacao { get; set; } = new List<CompraLocacao>();
    }
}
