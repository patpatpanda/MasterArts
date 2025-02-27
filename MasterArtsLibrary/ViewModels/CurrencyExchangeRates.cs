﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.ViewModels
{
    public class CurrencyExchangeRates
    {
        public int Id { get; set; }
        public string? Provider { get; set; }
        public string? Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal>? Rates { get; set; }
    }
}
