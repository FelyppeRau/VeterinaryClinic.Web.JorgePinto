using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }  

        public string LastName { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

    }
}
