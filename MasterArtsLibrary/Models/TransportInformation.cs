using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class TransportInformation
    {
        public string Module { get; set; }
        public string Type { get; set; }
        public string Direction { get; set; }
        public string ConsignmentNumber { get; set; }
        public string BlNumber { get; set; }
        public string Eta { get; set; }
        public string Etd { get; set; }
        public string PortOfLoading { get; set; }
        public string PortOfDischarge { get; set; }
        public string PortOfLoadingCode { get; set; }
        public string PortOfDischargeCode { get; set; }
        public string FinalDestination { get; set; }
        public string Vessel { get; set; }
        public string ContainerNumber { get; set; }
        public string ClientReference { get; set; }
        public int Packages { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string LastEvent { get; set; }
        public string LastEventCode { get; set; }
        public string LastEventDateTime { get; set; }
        public List<Event> Events { get; set; }
    }
    public class Event
    {
        public string BrickzCode { get; set; }
        public string BrickzName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}
