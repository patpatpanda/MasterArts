using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Models
{
    public class ContactFormModel
    {
        public string CompanyName { get; set; }
        [BindProperty]

        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Message is required.")]
        [MinLength(5, ErrorMessage = "Message must be at least 5 characters long.")]
        public string Message { get; set; }

        public string Honeypot { get; set; }
    }

}
