using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Sender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}
