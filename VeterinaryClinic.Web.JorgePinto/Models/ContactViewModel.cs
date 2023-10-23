using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Models
{
    public class ContactViewModel
    {
        //[Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
