using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class Usuario
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Senha { get; set; }

        public long? FuncionarioId { get; set; }
    }
}
