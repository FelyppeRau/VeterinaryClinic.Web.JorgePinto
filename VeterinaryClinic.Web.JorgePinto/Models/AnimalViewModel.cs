using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Models
{
    public class AnimalViewModel : Animal
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
