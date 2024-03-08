using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    
    public class Order
    {
        public int Id { get; set; }
        public string IntegrationId { get; set; }
        public string? CustomerOrderNumber { get; set; }
        public string? OrderReference { get; set; }
      
        public string Customer { get; set; }
        public string OrderType { get; set; }
        public string FreightService { get; set; }
        
        public string? DeliveryTimeFrom { get; set; }
        public string PickUpTimeFrom { get; set; }

        public int TransportModeCode { get; set; }
        public string DeliveryTimeTo { get; set; }
        
        public string? TermsOfDelivery { get; set; }

       
        public string ConsignorName { get; set; }
        
        public string ConsignorAddress1 { get; set; }
        public string? ConsignorAddress2 { get; set; }
        public string ConsignorCity { get; set; }
        //public string ConsignorCountry { get; set; }
        public string ConsignorZip { get; set; }
        public string ConsignorContact { get; set; }
        public string ConsignorPhone { get; set; }
        public string ConsignorEmail { get; set; }

        // Mottagare (consignee) attribut
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress1 { get; set; }
        [Display(Name = "Consignee Address 2")]
        public string? ConsigneeAddress2 { get; set; }
        public string ConsigneeCity { get; set; }
        ////public string ConsigneeCountry { get; set; }
        public string ConsigneeZip { get; set; }
        public string ConsigneeContact { get; set; }
        public string ConsigneePhone { get; set; }
        public string ConsigneeEmail { get; set; }

        public List<Goods> Goods { get; set; } = new List<Goods>();



        public Order()
        {
            IntegrationId = Guid.NewGuid().ToString();
        }
    }


}
