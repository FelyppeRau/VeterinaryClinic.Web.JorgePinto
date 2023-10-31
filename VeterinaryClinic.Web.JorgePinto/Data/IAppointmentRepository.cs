using System;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        public IQueryable GetAllWithUsers();

        Task<IQueryable<Appointment>> GetAppointmentAsync(string userName); // Dá-me todas as consultas de um determinado user


        Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName);

        Task<bool> AddItemToAppointmentAsync(AddItemViewModel model, string userName);

        
        Task DeleteDetailTempAsync(int id);        

        Task<bool> ConfirmAppointmentAsync(string userName, int id);


        Task<Appointment> GetAppointmentAsync(int id);

        Task<bool> IsDuplicateAppointmentAsync(int medicid, DateTime scheduleDate, DateTime time); // DUPLICIDADE ANTES DE CONFIRMAR

        Task<bool> ConfirmDuplicateAppointmentAsync(int medicid, DateTime scheduleDate, DateTime time);  // DUPLICIDADE AO CONFIRMAR


    }
}
