using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class Fornecedor
    {
        public long Id { get; set; }

        public string? CNPJ { get; set; }

        public string? RazaoSocial { get; set; }

        public string? NomeFantasia { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public long? EnderecoId { get; set; }
    }
}
