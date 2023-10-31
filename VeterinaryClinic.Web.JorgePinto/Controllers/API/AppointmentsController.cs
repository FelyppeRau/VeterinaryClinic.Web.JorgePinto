using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Web.JorgePinto.Data;

namespace VeterinaryClinic.Web.JorgePinto.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public IActionResult GetAppointments()
        { 
            return Ok(_appointmentRepository.GetAllWithUsers());
        }
    }
}
