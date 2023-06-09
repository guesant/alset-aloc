﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    public class Endereco
    {
        public long Id { get; set; }

        public string Pais { get; set; }

        public string CodigoPostal { get; set; }

        public string UF { get; set; }

        public string Cidade { get; set; }

        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }
    }
}
