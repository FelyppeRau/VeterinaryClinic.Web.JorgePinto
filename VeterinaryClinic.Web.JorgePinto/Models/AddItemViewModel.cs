using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace VeterinaryClinic.Web.JorgePinto.Models
{
    public class AddItemViewModel
    {

        [Display(Name = "Animal")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Animal.")]
        public int AnimalId { get; set; }


        public IEnumerable<SelectListItem> Animals { get; set; }  // VERIFICAR COMO SOMENTE APARECER NA COMBOX OS ANIMAIS DO USUARIO LOGADO

        
        [Display(Name = "Owner")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Owner.")]
        public int OwnerId { get; set; }

        public IEnumerable<SelectListItem> Owners { get; set; }  // VERIFICAR COMO SOMENTE APARECER NA COMBOX O OWNER


        [Display(Name = "Medic")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Medic.")]
        public int MedicId { get; set; }

        public IEnumerable<SelectListItem> Medics { get; set; }

        [Required]
        [Display(Name = "Schedule Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = false)]        
        public DateTime ScheduleDate { get; set; }

        [Required]
		[DataType(DataType.Time)]
		//[Display(Name = "Time")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = false)]
		public DateTime Time { get; set; }
    }
}
