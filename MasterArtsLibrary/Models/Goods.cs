using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string? ArticleCode { get; set; }
        public string? MarksNumbers { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }

        public string? PackageType { get; set; }
        public double VolumetricWeight { get; set; }
        public double NetWeight { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Volume { get; set; }

    }

}
