using Newtonsoft.Json;
using System.Collections.Generic;

namespace MasterArtsLibrary.Models
{




    public class ApiResponseInland
    {
        public float total { get; set; }
        public float usdExchangeRate { get; set; }
        public float eurExchangeRate { get; set; }
        public string currency { get; set; }
        public float fuelSurcharge { get; set; }
        public float fuelSurchargePercentage { get; set; }
        public float co2 { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
    }
    public class ApiResponse
    {
        public List<Total> Totals { get; set; }
        public List<Rate> Rates { get; set; }
        public int TransitTime { get; set; }
        public Sailing Sailing { get; set; }
        public Agent Agent { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public double Co2 { get; set; }
        public bool OnRequest { get; set; }
    }

    public class Total
    {
        public string Currency { get; set; }
        public double Sum { get; set; }
        public decimal ExchangeRate { get; set; }
    }

    public class Rate
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal RateValue { get; set; }
        public decimal MinimumSum { get; set; }
        public decimal Sum { get; set; }
        public decimal SumExchangeRate { get; set; }
        public string CalculationMethod { get; set; }
        public decimal Multiplier { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Tags { get; set; }
    }

    public class Sailing
    {
        public string Vessel { get; set; }
        public string VoyageNumber { get; set; }
        public string FromCode { get; set; }
        public string FromDescription { get; set; }
        public string ToCode { get; set; }
        public string ToDescription { get; set; }
        public string TransshipmentCode { get; set; }
        public string TransshipmentDescription { get; set; }
        public string GatewayCode { get; set; }
        public string GatewayDescription { get; set; }
        public DateTime Closing { get; set; }
        public DateTime Etd { get; set; }
        public DateTime Eta { get; set; }
        public string Remarks { get; set; }
    }

    public class Agent
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }

    public class Dimension
    {
        [JsonProperty("pcs")]
        public int Pcs { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("stackable")]
        public bool Stackable { get; set; }
    }

    public class Option
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ShippingRequest
    {
        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("importExport")]
        public string ImportExport { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = "lcl";
        [JsonIgnore]
        public string UserSelection { get; set; }

        [JsonProperty("fromCode")]
        public string FromCode { get; set; }

        [JsonProperty("toCode")]
        public string ToCode { get; set; }


        [JsonProperty("routingCode")]
        public string? RoutingCode { get; set; }
       
        [JsonProperty("inlandZipCode")]
        public string? InlandZipCode { get; set; }

        [JsonProperty("packages")]
        public int Packages { get; set; }

        [JsonProperty("packageType")]
        public string PackageType { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("volume")]
        public float Volume { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
        

        [JsonProperty("dimensions")]
        public List<Dimension>? Dimensions { get; set; }

        //[JsonProperty("options")]
        //public List<Option> Options { get; set; }
    }

}