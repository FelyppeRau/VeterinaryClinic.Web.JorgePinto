using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class AppointmentDetail : IEntity
    {

        public int Id { get; set; }


        [Required]
        public Owner Owner { get; set; }


        [Required]
        public Animal Animal { get; set; }


        [Required]
        public Medic Medic { get; set; }


        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = false)]
        public DateTime ScheduleDate { get; set; }

        [Required]
		[DataType(DataType.Time)]
		//[Display(Name = "Time")]
		//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = false)]
		public DateTime Time { get; set; }
    }
}
