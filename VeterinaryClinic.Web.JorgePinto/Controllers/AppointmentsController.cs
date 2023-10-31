using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{
    [Authorize(Roles = "Medic, Owner")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IMedicRepository _medicRepository;
        private readonly IMailHelper _mailHelper;

        public AppointmentsController(IAppointmentRepository appointmentRepository, IOwnerRepository ownerRepository,
                                      IAnimalRepository animalRepository, IMedicRepository medicRepository, IMailHelper mailHelper)
        {
            _appointmentRepository = appointmentRepository;
            _ownerRepository = ownerRepository;
            _animalRepository = animalRepository;
            _medicRepository = medicRepository;
            _mailHelper = mailHelper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _appointmentRepository.GetAppointmentAsync(this.User.Identity.Name);

            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            var model = await _appointmentRepository.GetDetailTempsAsync(this.User.Identity.Name);

            return View(model);
        }

        public IActionResult AddAppointment()
        {
            var model = new AddItemViewModel
            {
                Owners = _ownerRepository.GetComboOwners(),
                Animals = _animalRepository.GetComboAnimals(),
                Medics = _medicRepository.GetComboMedics(),
                ScheduleDate = DateTime.Today,  // como apresenta o calendário
                Time = DateTime.Today,
            };

            //if (!User.IsInRole("Medic"))
            //{
            //    // Se não for um médico, obtenha o ID do usuário logado
            //    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //    // Preencha a lista de proprietários apenas com o usuário logado
            //    model.Owners = _ownerRepository.GetComboOwners().Where(owner => owner.Value == currentUserId);
            //}
            //else
            //{
            //    // Caso contrário, preencha a lista de proprietários normalmente
            //    model.Owners = _ownerRepository.GetComboOwners();
            //}

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddAppointment(AddItemViewModel model)
        {

            if (ModelState.IsValid)
            {
                bool response = false;

                if (model.ScheduleDate >= DateTime.Today)
                {
                    response = await _appointmentRepository.AddItemToAppointmentAsync(model, this.User.Identity.Name);

                    if (!response)
                    {
                        ViewBag.ErrorMessage = "Unavailable Schedules!";
                                                

                        return View(model);
                    }

                    return RedirectToAction("Create");
                }
                else
                {
                    ModelState.AddModelError("Invalid Date", "Schedule with availability only from today.");
                }

            }

            return View(model);
        }



        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }

            await _appointmentRepository.DeleteDetailTempAsync(id.Value);

            return RedirectToAction("Create");
        }


        public IActionResult AppointmentNotFound()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmAppointment(int id)
        {
            var response = await _appointmentRepository.ConfirmAppointmentAsync(this.User.Identity.Name, id);
                       
            if (response)
            {
                return RedirectToAction("Create");
            }

            ViewBag.ErrorMessage = "Unavailable Schedules!";

            return RedirectToAction("Create");
            
        }

        // GET: Medics/Delete/5       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }

            var appointment = await _appointmentRepository.GetByIdAsync(id.Value);

            if (appointment == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            try
            {
                await _appointmentRepository.DeleteAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"The {appointment.Animal} is probably linked to some schedule!";
                    ViewBag.ErrorMessage = $"The {appointment.Animal} cannot be deleted, as there are appointments linked.</br></br>" +
                         $"First, try deleting all linked appointments and after deleting the Medic again.";
                }
            }

            return View("Error");
        }
    }
}
