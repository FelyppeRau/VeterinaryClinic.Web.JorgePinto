﻿using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Models
{
    public class ChangePasswordViewModel
    {

        [Required]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }


        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [Required]
        [Compare("NewPassword")]  // Compara com a password digitada anteriomente
        public string Confirm { get; set; }
    }
}
