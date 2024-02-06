using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public enum ShippingServiceName
    {
        OceanFreight,
        Airfreight,
        ExpressKurir,
        Road,
        Trainfreight,
        StandardKurir,
        Linehaul
    }

    public class ShippingService
    {
        public int Id { get; set; }
        public ShippingServiceName OceanFreight { get; set; }
        public DateTime LoadingDate { get; set; }
        public DateTime UnloadingDate { get; set; }
        public string LoadingTimeWindow { get; set; }
        public string UnloadingTimeWindow { get; set; }
    }

}
