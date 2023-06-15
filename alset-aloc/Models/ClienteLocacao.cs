using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    internal class ClienteLocacao
    {
        public long Id { get; set; }

        public long ClienteId { get; set; }

        public long LocacaoId { get; set; }
    }
}
