using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AppointmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

       

        public async Task<bool> AddItemToAppointmentAsync(AddItemViewModel model, string userName)
        {
            // NESSA FASE VERIFICAMOS SE O USER, OWNER, ANIMAL, MEDIC AINDA CONSTAM NA DB
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var owner = await _context.Owners.FindAsync(model.OwnerId);
            if (owner == null)
            {
                return false;
            }

            var animal = await _context.Animals.FindAsync(model.AnimalId);
            if (animal == null)
            {
                return false;
            }

            var medic = await _context.Medics.FindAsync(model.MedicId);
            if (medic == null)
            {
                return false;
            }

            // TRAVA SE HOUVER UM AGENDAMENTO PARA O MESMO MÉDICO NO MESMO DIA E HORÁRIO (*** NA AppointmentDetailsTemp ANTES DE CONFIRMAR ***)
            var isDuplicateAppointment = await IsDuplicateAppointmentAsync(medic.Id, model.ScheduleDate, model.Time);
            if (isDuplicateAppointment)
            {    
              return false;                
            }              
                            

            var appointmentDetailTemp = await _context.AppointmentDetailsTemps 
                .Where(adt => adt.User == user && adt.Owner == owner && adt.Animal == animal && adt.Medic == medic)
                .FirstOrDefaultAsync();


            if (appointmentDetailTemp == null)
            {
                appointmentDetailTemp = new AppointmentDetailTemp
                {
                    Owner = owner,
                    Animal = animal,
                    Medic = medic,
                    ScheduleDate = model.ScheduleDate,
                    Time = model.Time,
                    User = user,
                };


                _context.AppointmentDetailsTemps.Add(appointmentDetailTemp);
            }
            else  // Aqui está adicionando cada vez que criamos uma nova consulta
            {
                appointmentDetailTemp.Owner = owner;
                appointmentDetailTemp.Animal = animal;
                appointmentDetailTemp.Medic = medic;
                //model.ScheduleDate = model.ScheduleDate;
            }


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmAppointmentAsync(string userName, int id)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var appointmentTmp = await _context.AppointmentDetailsTemps
                .Include(o => o.Owner)
                .Include(o => o.Animal)
                .Include(o => o.Medic)
                .Where(o => o.User == user && o.Id == id) // O id Permite confirmar um em qq lugar da lista
                .OrderBy(o => o.ScheduleDate)
                //.ToListAsync(); // Busca todos os ítens!
                .FirstOrDefaultAsync(); // Busca um por um!

            if (appointmentTmp == null) 
            {
                return false;
            }

            // TRAVA SE HOUVER UM AGENDAMENTO PARA O MESMO MÉDICO NO MESMO DIA E HORÁRIO (*** NA Appointment AO CONFIRMAR ***)
            var confirmedAppointment = await ConfirmDuplicateAppointmentAsync(appointmentTmp.Medic.Id, appointmentTmp.ScheduleDate, appointmentTmp.Time);
            if (confirmedAppointment)
            {
                return false;
            }

            var detail = new AppointmentDetail
            {
                Owner = appointmentTmp.Owner,
                Animal = appointmentTmp.Animal,
                Medic = appointmentTmp.Medic,
                ScheduleDate = appointmentTmp.ScheduleDate,
                Time = appointmentTmp.Time
            };

            var appointment = new Appointment
            {
                Owner = appointmentTmp.Owner,
                Animal = appointmentTmp.Animal,
                Medic = appointmentTmp.Medic,
                ScheduleDate = appointmentTmp.ScheduleDate,
                Time = appointmentTmp.Time,
                User = user,
                Items = new List<AppointmentDetail> { detail }
            };

            await CreateAsync(appointment); // Salvar em Appointment

            _context.AppointmentDetailsTemps.Remove(appointmentTmp); // Remover de Temporários

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConfirmDuplicateAppointmentAsync(int medicid, DateTime scheduleDate, DateTime time)
        {
            return await _context.AppointmentDetails
               .AnyAsync(a => a.Medic.Id == medicid && a.ScheduleDate == scheduleDate && a.Time == time);
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var appointmentDetailTemp = await _context.AppointmentDetailsTemps.FindAsync(id);
            if (appointmentDetailTemp == null)
            {
                return;
            }

            _context.AppointmentDetailsTemps.Remove(appointmentDetailTemp);
            await _context.SaveChangesAsync();
        }



        public async Task<IQueryable<Appointment>> GetAppointmentAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Medic")) // SE FOR O MÉDICO LOGADO DÁ-ME TODAS AS CONSULTAS                                                                   
            {
                return _context.Appointments
                    .Include(i => i.User)  // Mostra o User que marcou a consulta
                    .Include(o => o.Items) // Buscar
                    .ThenInclude(i => i.Owner) // Mostrar
                    .Include(o => o.Items)  // Buscar
                    .ThenInclude(i => i.Animal)  // Mostrar
                    .Include(o => o.Items)  // Buscar
                    .ThenInclude(i => i.Medic)  // Mostrar
                    .OrderBy(o => o.ScheduleDate);
            }

            return _context.Appointments // CASO NÃO SEJA O MEDIC DÁ-ME AS CONSULTAS DE QUEM ESTIVER LOGADO. LIMITAR O ACESSO AO CRUD APPOINTMENTS
                .Include(o => o.Items)  // Buscar
                .ThenInclude(i => i.Owner)  // Mostrar
                .Include(o => o.Items)  // Buscar
                .ThenInclude(i => i.Animal)  // Mostrar
                .Include(o => o.Items)  // Buscar
                .ThenInclude(i => i.Medic)  // Mostrar
                .Where(o => o.User == user)
                .OrderBy(o => o.ScheduleDate);
        }

        public async Task<Appointment> GetAppointmentAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.AppointmentDetailsTemps
                .Include(o => o.Owner)
                .Include(o => o.Animal)
                .Include(o => o.Medic)
                .Where(o => o.User == user)
                .OrderBy(o => o.ScheduleDate);


        }

        public async Task<bool> IsDuplicateAppointmentAsync(int medicid, DateTime scheduleDate, DateTime time)
        {
            return await _context.AppointmentDetailsTemps
                .AnyAsync(a => a.Medic.Id == medicid && a.ScheduleDate == scheduleDate && a.Time == time);
        }
    }
}
