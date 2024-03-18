using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Rate
    {
    }
    public class Total
    {
        public string Currency { get; set; }
        public decimal Sum { get; set; }
        public decimal ExchangeRate { get; set; }
    }

    public class ApiResponse
    {
        public List<Total> Totals { get; set; }
        // Lägg till andra delar av svaret här om nödvändigt
    }
    public class Dimension
    {
        public int Pcs { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public bool Stackable { get; set; }
    }

    public class Option
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ShippingRequest
    {
        public string Module { get; set; }
        public string ImportExport { get; set; }
        public string Type { get; set; }
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public string RoutingCode { get; set; }
        public bool Imo { get; set; }
        public string InlandZipCode { get; set; }
        public int Packages { get; set; }
        public string PackageType { get; set; }
        public int Weight { get; set; }
        public double Volume { get; set; }
        public string Date { get; set; }
        public List<Dimension> Dimensions { get; set; }
        public List<Option> Options { get; set; }
    }
}
