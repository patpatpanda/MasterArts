using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public enum RateType
    {
        LCL,
        FCL
    }
    public class FclRate
    {
        public string Module { get; set; } 
        public string ImportExport { get; set; } 
        public string Type { get; set; } 
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public string? RoutingCode { get; set; }
        public string InlandZipCode { get; set; }
        public int NoOfContainers { get; set; } 
        public double Weight { get; set; } 
        public string ContainerMode { get; set; } 
        public string Date { get; set; } 
    }
}
