﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace matamata.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int TorneioId { get; set; }
    }
}