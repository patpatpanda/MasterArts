using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Ups
    {
    }

    public class RateResponse
    {
        public ResponseDetails Response { get; set; }
        public List<RatedShipment> RatedShipment { get; set; }
    }

    public class ResponseDetails
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<Alert> Alerts { get; set; }
        public TransactionReference TransactionReference { get; set; }
    }

    public class ResponseStatus
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class Alert
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }

    public class RatedShipment
    {
        public Service? Service { get; set; }
        public string? RateChart { get; set; }
        public BillingWeight? BillingWeight { get; set; }
        public Charge? TransportationCharges { get; set; }
        public Charge? BaseServiceCharge { get; set; }
        public List<Charge>? ItemizedCharges { get; set; }
        public Charge? TotalCharges { get; set; }
        public Charge? TotalChargesWithTaxes { get; set; }
    }

    public class Service
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }

    public class BillingWeight
    {
        public UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public string? Weight { get; set; }
    }

    public class Charge
    {
        public string? CurrencyCode { get; set; }
        public string? MonetaryValue { get; set; }
    }








    public class RateRequest
    {
        public RequestDetails Request { get; set; } = new RequestDetails();
        public Shipment Shipment { get; set; } = new Shipment();
    }

    public class RequestDetails
    {
        public TransactionReference TransactionReference { get; set; } = new TransactionReference();
    }

    public class TransactionReference
    {
        public string CustomerContext { get; set; }
    }

    public class Shipment
    {
        public Party Shipper { get; set; }
        public Party ShipTo { get; set; }
        public ServiceDetails Service { get; set; }
        public string NumOfPieces { get; set; }
        public PackageDetails Package { get; set; }
    }

    public class Party
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public List<string> AddressLine { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    }

    

    public class PackageDetails
    {
        public PackagingType PackagingType { get; set; }
        public Dimensions Dimensions { get; set; }
        public PackageWeight PackageWeight { get; set; }
    }
    public class ServiceDetails
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }


   
    public class PackagingType
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }

    public class Dimensions
    {
        [JsonProperty("UnitOfMeasurement")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        [JsonProperty("Length")]
        public string Length { get; set; }

        [JsonProperty("Width")]
        public string Width { get; set; }

        [JsonProperty("Height")]
        public string Height { get; set; }
    }

    public class PackageWeight
    {
        [JsonProperty("UnitOfMeasurement")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        [JsonProperty("Weight")]
        public string Weight { get; set; }
    }

    public class UnitOfMeasurement
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }

    // You can reuse the Service and TotalCharges classes if they match the response structure

}