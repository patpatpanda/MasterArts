using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string? ShipFromCompany { get; set; }
        public string? ShipFromAddress { get; set; }

        public string? ShipToCompany { get; set; }
        public string? ShipToAddress { get; set; }

        public int? NumberOfBoxes { get; set; }
        public double? WeightOfBoxes { get; set; }

        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? CustomerEmail { get; set; }
        public DateTime? PickUpDate { get; set; }
    }
}
