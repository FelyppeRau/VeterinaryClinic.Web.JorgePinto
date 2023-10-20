using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace VeterinaryClinic.Web.JorgePinto.Models
{
    [Authorize(Roles = "Admin, Medic, Owner")]
    public class ChangeUserViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
