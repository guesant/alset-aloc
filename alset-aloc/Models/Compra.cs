using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    internal class Compra
    {
        public long Id { get; set; }

        public DateTime? DataCompra { get; set; }

        public string? NumeroNota { get; set; }
        public long? Quantidade { get; set; }

        public long? ProdutoId { get; set; }

        public long? FornecedorId { get; set; }
    }
}
