using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int SenderId { get; set; } // Egenskap för att lagra avsändarens ID
        public int ReceiverId { get; set; } // Egenskap för att lagra mottagarens ID

        // Navigerings egenskaper
        public Sender Sender { get; set; }
        public Receiver Receiver { get; set; }
    }
}
