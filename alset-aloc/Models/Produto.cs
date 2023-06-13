using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class Produto
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public double Preco { get; set; }

        public double Estoque { get; set; }
    }
}
