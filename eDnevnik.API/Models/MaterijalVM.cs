﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDnevnik.API.Models
{
    public class MaterijalVM
    {
        public int MaterijalId { get; set; }
        public string Naziv { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public string Datum { get; set; }
    }
}
