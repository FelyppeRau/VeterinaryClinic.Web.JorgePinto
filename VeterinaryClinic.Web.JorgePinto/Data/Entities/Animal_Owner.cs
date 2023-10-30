using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection.Emit;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class Animal_Owner : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Animal")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Name { get; set; }


        [Required]
        [MaxLength(1, ErrorMessage = "Enter M for Male or F for Female")]
        public string Sex { get; set; }


        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Breed { get; set; }

        [Required]
        public string Species { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }


        public string ImageFullPath => ImageId == Guid.Empty
             ? $"/images/noimage.png"
             : $"https://supershop.blob.core.windows.net/animals/{ImageId}";


        [Display(Name = "Animal")]
        public string FullAnimal => $"{Name} - {Breed}";
    }
}
