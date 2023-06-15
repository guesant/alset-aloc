using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    internal class Parcela
    {
        public long Id { get; set; }

        public DateTime DataVencimento { get; set; }

        public double Valor { get; set; }

        public string? FormaPagamento { get; set; }

        public int? Numero { get; set; }

        public string? Tipo { get; set; }

        public bool? Status { get; set; }

        public long? RecebimentoId { get; set; }

        public long? PagamentoId { get; set; }
    }
}
