using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class MrRate
    {
        public int Id { get; set; }
        public string? Provider { get; set; }
        public string? Base { get; set; }
        public DateTime Date { get; set; }
    }
}
