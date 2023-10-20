using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace VeterinaryClinic.Web.JorgePinto.Models
{
    [Authorize(Roles = "Admin")]
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        [Required]
        [MinLength(6)] //Mesma qtde. no Startup
        public string Password { get; set; }


        [Required]
        [Compare("Password")]  // Compara com a password digitada anteriomente
        public string Confirm { get; set; }
    }
}
