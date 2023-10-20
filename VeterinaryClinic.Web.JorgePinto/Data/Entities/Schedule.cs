using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Web.JorgePinto.Data.Entities
{
    public class Schedule : IEntity
    {

        public int Id { get; set; }


        [Required]
        public string Medic { get; set; }

        [Required]
        public string Animal { get; set; }

        [Required]
        [Display(Name = "Schedule Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime ScheduleDate { get; set; }

        public User User { get; set; }

    }   
}
