using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
