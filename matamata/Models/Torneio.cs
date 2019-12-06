using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace matamata.Models
{
    public class Torneio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Rodada { get; set; }
        public string VencedorNome { get; set; }
    }
}