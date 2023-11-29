using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    internal class Locacao
    {
        public long Id { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime? DataDevolucaoPrevista { get; set;  }

        public DateTime? DataDevolucaoEfetivada { get; set; }

        public bool Status { get; set; }

        public long? VeiculoId { get; set; }

        public long? FuncionarioId { get; set; }

        public long? ClienteId { get; set; }

        public double ValorDiaria { get; set; }
    }
}
