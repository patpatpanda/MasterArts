using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Consignor
    {
        public int Id { get; set; }
        public string ConsignorName { get; set; }
        public string ConsignorAddress1 { get; set; }
        public string ConsignorAddress2 { get; set; }
        public string ConsignorCity { get; set; }
        public string ConsignorCountry { get; set; }
        public string ConsignorZip { get; set; }
        public string ConsignorContact { get; set; }
        public string ConsignorPhone { get; set; }
        public string ConsignorEmail { get; set; }
    }
}
