using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class Appointment : IEntity
    {

        public int Id { get; set; }

        
        [Required]
        public Owner Owner { get; set; }


        [Required]
        public Animal Animal { get; set; }

        
        [Required]
        public Medic Medic { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime ScheduleDate { get; set; }

        [Required]
		[DataType(DataType.Time)]
		//[Display(Name = "Time")]
		//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = false)]
		public DateTime Time { get; set; }

        [Required]
        public User User { get; set; }

        public IEnumerable<AppointmentDetail> Items { get; set; }
    }
}
