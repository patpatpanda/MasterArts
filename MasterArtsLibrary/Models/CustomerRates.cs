using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class CustomerRates
    {
        public int Id { get; set; }
        public string? CustomerOrderNumber { get; set; }
        public List<Total>? Totals { get; set; }
        public List<Rate>? Rates { get; set; }
        public int? TransitTime { get; set; }
        public Sailing? Sailing { get; set; }
        public Agent? Agent { get; set; }
        public string? ValidFrom { get; set; }
        public string? ValidTo { get; set; }
        public double? Co2 { get; set; }
        public bool? OnRequest { get; set; }
        public ShippingRequest? ShippingRequest { get; set; }
    }
}
