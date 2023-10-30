using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class Owner : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Owner")]
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

        public string Address { get; set; }

        public User User { get; set; }

        public IEnumerable<Animal> Animals { get; set; }


        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }


        public string ImageFullPath => ImageId == Guid.Empty
             ? $"/images/noimage.png"
             : $"https://supershop.blob.core.windows.net/owners/{ImageId}";

       
    }
}
