using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }

        public static implicit operator decimal(CurrencyRate? v)
        {
            throw new NotImplementedException();
        }
    }
}
