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
        public string DeliveryTimeFrom { get; set; } // Ändra datatypen till string
        public string DeliveryTimeTo { get; set; }   // Ändra datatypen till string
        public string PickUpTimeFrom { get; set; }   // Ändra datatypen till string
        public string PickUpTimeTo { get; set; }

        // Avsändare (consignor) attribut
        public Consignor Consignor { get; set; }

        // Mottagare (consignee) attribut
        public Consignee Consignee { get; set; }

        public Goods Goods { get; set; }



        public Order()
        {
            IntegrationId = Guid.NewGuid().ToString();
        }
    }


}
