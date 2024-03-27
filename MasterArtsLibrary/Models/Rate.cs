using Newtonsoft.Json;
using System.Collections.Generic;

namespace MasterArtsLibrary.Models
{
    public class Rate
    {
        // Om Rate-klassen ska ha egenskaper, följ samma mönster.
    }

    public class Total
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("sum")]
        public decimal Sum { get; set; }

        [JsonProperty("exchangeRate")]
        public decimal ExchangeRate { get; set; }
    }

    public class ApiResponse
    {
        [JsonProperty("totals")]
        public List<Total> Totals { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
        // Lägg till andra delar av svaret här om nödvändigt
    }
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

        [JsonProperty("imo")]
        public bool Imo { get; set; }

        [JsonProperty("inlandZipCode")]
        public string InlandZipCode { get; set; }

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
        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("dimensions")]
        public List<Dimension>? Dimensions { get; set; }

        //[JsonProperty("options")]
        //public List<Option> Options { get; set; }
    }

}
