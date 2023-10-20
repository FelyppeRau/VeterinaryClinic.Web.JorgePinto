using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{
    
    public class SchedulesController : Controller
    {

        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserHelper _userHelper;

        public SchedulesController(IScheduleRepository scheduleRepository,
                                   IUserHelper userHelper)
        {
            _scheduleRepository = scheduleRepository;
            _userHelper = userHelper;
        }

        // GET: Schedules
        public IActionResult Index()
        {
            return View(_scheduleRepository.GetAll().OrderBy(p => p.ScheduleDate));
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }

            var schedule = await _scheduleRepository.GetByIdAsync(id.Value);

            if (schedule == null)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                schedule.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _scheduleRepository.CreateAsync(schedule);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }

            var schedule = await _scheduleRepository.GetByIdAsync(id.Value);

            if (schedule == null)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                    schedule.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _scheduleRepository.UpdateAsync(schedule);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _scheduleRepository.ExistAsync(schedule.Id))
                    {
                        return new NotFoundViewResult("ScheduleNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }

            var medic = await _scheduleRepository.GetByIdAsync(id.Value);
            if (medic == null)
            {
                return new NotFoundViewResult("ScheduleNotFound");
            }

            return View(medic);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medic = await _scheduleRepository.GetByIdAsync(id);
            await _scheduleRepository.DeleteAsync(medic);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ScheduleNotFound()
        {
            return View();
        }
    }
}
