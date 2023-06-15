using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class Pagamento
    {
        public long Id { get; set; }

        public string Descricao { get; set; }

        public double? Valor { get; set; }

        public DateTime? DataVencimento { get; set; }

        public DateTime? DataCredenciamento { get; set; }

        public string? Credor { get; set; }

        public int? Parcelas { get; set; }

        public long? CompraId { get; set; }
    }
}
