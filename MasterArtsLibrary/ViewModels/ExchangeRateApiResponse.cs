﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.ViewModels
{
    public class ExchangeRateApiResponse
    {
        public string? Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal>? Rates { get; set; }
    }
}
