using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Consignee
    {
        public int Id { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress1 { get; set; }
        [Display(Name = "Consignee Address 2")]
        public string? ConsigneeAddress2 { get; set; }
        public string ConsigneeCity { get; set; }
        public string ConsigneeCountry { get; set; }
        public string ConsigneeZip { get; set; }
        public string ConsigneeContact { get; set; }
        public string ConsigneePhone { get; set; }
        public string ConsigneeEmail { get; set; }
    }
}
