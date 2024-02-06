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
        public string CustomerOrderNumber { get; set; }
        public string OrderReference { get; set; }
        public Customer Customer { get; set; }
        public ShippingService ShippingService { get; set; }
        public OrderDetails OrderDetails { get; set; }


    }
}
