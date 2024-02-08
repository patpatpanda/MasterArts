using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    
    public class Order
    {
         public int Id { get; set; }
        public string IntegrationId { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderReference { get; set; }
        public string Customer { get; set; }
        public string FreightService { get; set; }
        public DateTime DeliveryTimeFrom { get; set; }
        public DateTime DeliveryTimeTo { get; set; }
        public DateTime PickUpTimeFrom { get; set; }
        public DateTime PickUpTimeTo { get; set; }

        // Avsändare (consignor) attribut
        public Consignor Consignor { get; set; }

        // Mottagare (consignee) attribut
        public Consignee Consignee { get; set; }

        // Leveransinformation (delivery) attribut
        public Delivery Delivery { get; set; }


        public Order()
        {
            IntegrationId = Guid.NewGuid().ToString();
        }
    }


}
