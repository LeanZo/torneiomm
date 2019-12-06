using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace matamata.Models
{
    public class Partida
    {
        public int Id { get; set; }
        public string Time1Nome { get; set; }
        public string Time2Nome { get; set; }
        public int Gols1 { get; set; }
        public int Gols2 { get; set; }
        public string VencedorNome { get; set; }
        public int TorneioId { get; set; }
        public int Rodada { get; set; }
        public int Slot { get; set; }
    }
}