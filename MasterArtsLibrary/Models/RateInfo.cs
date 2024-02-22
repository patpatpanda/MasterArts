using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class RateInfo
    {
        public string CurrencyPairKey { get; set; }
        public decimal Rate { get; set; }
        public decimal? ChangePercent { get; set; }
        public bool YesterdayRateExists { get; set; }
    }

}
