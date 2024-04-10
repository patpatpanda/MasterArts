using MasterArtsLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.ViewModels
{
    public class InlandViewModel
    {
        [JsonProperty("module")]
        public string? Module { get; set; }

        [JsonProperty("importExport")]
        public string? ImportExport { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; } = "lcl";
        [JsonIgnore]
        public string? UserSelection { get; set; } = "inlandtransport";





        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("packages")]
        public int? Packages { get; set; }

        

        [JsonProperty("weight")]
        public int? Weight { get; set; }

        [JsonProperty("volume")]
        public float? Volume { get; set; }

       
       

        [JsonProperty("dimensions")]
        public List<Dimension>? Dimensions { get; set; }
    }
}
