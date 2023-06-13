using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    internal class Veiculo
    {
        public long Id { get; set; }

        public string Modelo { get; set; }

        public string Marca { get; set; }

        public int Ano { get; set;  }

        public string Placa { get; set; }

        public string NumeroChassi { get; set; }

        public string Cor { get; set; }

        public DateTime DataCompra { get; set; }

        public string Descricao { get; set; }
    }
}
