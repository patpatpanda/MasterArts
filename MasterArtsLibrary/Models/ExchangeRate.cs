using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string? BaseCurrency { get; set; }
        public string? TargetCurrency { get; set; }
        public decimal? Rate { get; set; }
    }
}
