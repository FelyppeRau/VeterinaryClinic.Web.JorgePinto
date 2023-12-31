﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class Animal : IEntity
    {

        public int Id { get; set; }

        [Required]
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


        public User User { get; set; }

        public Owner Owner { get; set; }

        //[Display(Name = "Owner")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a Owner.")]
        //public int OwnerId { get; set; }

        //public IEnumerable<SelectListItem> Owners { get; set; }  // VERIFICAR COMO SOMENTE APARECER NA COMBOX O OWNER

        [Required]
        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }


        public string ImageFullPath => ImageId == Guid.Empty
             ? $"/images/noimage.png"
             : $"https://supershop.blob.core.windows.net/animals/{ImageId}";


        [Display(Name = "Animal")]
        public string FullAnimal => $"{Name} - {Breed}";


       



        //public decimal? Weight { get; set; }


        //[Required]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid Email")]
        //[Display(Name = "Owner Email")]
        //public string Email { get; set; }

    }
}
