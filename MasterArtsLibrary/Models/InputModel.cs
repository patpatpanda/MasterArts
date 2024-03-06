using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class InputModel
    {
        // Övriga fält...

        [Required]
        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }
    }
}
