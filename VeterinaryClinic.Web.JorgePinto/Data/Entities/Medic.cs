using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class Medic : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Veterinarians")]
        public string OMV { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Display(Name = "Medic")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        [Display(Name = "Cell Phone")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "The phone number must be exactly 9 digits.")]
        public string CellPhone { get; set; }


        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


        
        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }


        public string ImageFullPath => ImageId == Guid.Empty
             ? $"/images/noimage.png"
             : $"https://supershop.blob.core.windows.net/medics/{ImageId}";

        public User User { get; set; }

    }
}
