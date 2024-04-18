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
        // Andra egenskaper...
        public Party Shipper { get; set; } = new Party();
        public Party ShipTo { get; set; } = new Party();
        public Party ShipFrom { get; set; } = new Party();
        public PaymentDetails PaymentDetails { get; set; } = new PaymentDetails();
        public ServiceDetails Service { get; set; } = new ServiceDetails();
        public string NumOfPieces { get; set; } 
        public PackageDetails Package { get; set; } = new PackageDetails();
    }


    public class RequestDetails
    {
        public TransactionReference? TransactionReference { get; set; }
    }

    public class TransactionReference
    {
        public string? CustomerContext { get; set; }
    }

    public class ShipmentDetails
    {
        public Party? Shipper { get; set; }
        public Party? ShipTo { get; set; }
        public Party? ShipFrom { get; set; }
        public PaymentDetails? PaymentDetails { get; set; }
        public ServiceDetails? Service { get; set; }
        public string? NumOfPieces { get; set; }
        public PackageDetails? Package { get; set; }
    }

    public class Party
    {
        public string? Name { get; set; }
        public string? ShipperNumber { get; set; }
        public Address? Address { get; set; }
    }

    public class Address
    {
        public List<string>? AddressLine { get; set; }
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryCode { get; set; }
    }

    public class PaymentDetails
    {
        public ShipmentCharge? ShipmentCharge { get; set; }
    }

    public class ShipmentCharge
    {
        public string? Type { get; set; }
        public BillShipper? BillShipper { get; set; }
    }

    public class BillShipper
    {
        public string? AccountNumber { get; set; }
    }

    public class ServiceDetails
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }

    public class PackageDetails
    {
        public SimpleRate? SimpleRate { get; set; }
        public PackagingType? PackagingType { get; set; }
        public Dimensions? Dimensions { get; set; }
        public PackageWeight? PackageWeight { get; set; }
    }

    public class SimpleRate
    {
        public string? Description { get; set; }
        public string? Code { get; set; }
    }

    public class PackagingType
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }

    public class Dimensions
    {
        public UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public string? Length { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
    }

    public class PackageWeight
    {
        public UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public string? Weight { get; set; }
    }

    public class UnitOfMeasurement
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }

    // You can reuse the Service and TotalCharges classes if they match the response structure

}
