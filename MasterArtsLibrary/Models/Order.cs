using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public enum ShippingServiceName
    {
        OceanFreight,
        Airfreight,
        ExpressKurir,
        Road,
        Trainfreight,
        StandardKurir,
        Linehaul
    }
    public class Order
    {
        public int Id { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderReference { get; set; }
        public string Customer { get; set; }
        public ShippingServiceName FreightService { get; set; }
        public DateTime DeliveryTimeFrom { get; set; }
        public DateTime DeliveryTimeTo { get; set; }
        public DateTime PickUpTimeFrom { get; set; }
        public DateTime PickUpTimeTo { get; set; }

        // Avsändare (consignor) attribut
        public string ConsignorName { get; set; }
        public string ConsignorAddress1 { get; set; }
        public string ConsignorAddress2 { get; set; }
        public string ConsignorCity { get; set; }
        public string ConsignorCountry { get; set; }
        public string ConsignorZip { get; set; }
        public string ConsignorContact { get; set; }
        public string ConsignorPhone { get; set; }
        public string ConsignorEmail { get; set; }

        // Mottagare (consignee) attribut
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress1 { get; set; }
        public string ConsigneeAddress2 { get; set; }
        public string ConsigneeCity { get; set; }
        public string ConsigneeCountry { get; set; }
        public string ConsigneeZip { get; set; }
        public string ConsigneeContact { get; set; }
        public string ConsigneePhone { get; set; }
        public string ConsigneeEmail { get; set; }

        // Leveransinformation (delivery) attribut
        public string DeliveryName { get; set; }
        public string DeliveryAddress1 { get; set; }
        public string DeliveryAddress2 { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryZip { get; set; }
        public string DeliveryContact { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryEmail { get; set; }
    }


}
