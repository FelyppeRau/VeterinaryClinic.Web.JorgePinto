using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{
    
    //[Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserHelper _userHelper;

        public RoomsController(IRoomRepository roomRepository,
                               IUserHelper userHelper)
        {
            _roomRepository = roomRepository;
            _userHelper = userHelper;
        }

        // GET: Rooms
        public IActionResult Index()
        {
            return View(_roomRepository.GetAll().OrderBy(p => p.Id));
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            var room = await _roomRepository.GetByIdAsync(id.Value);

            if (room == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                //room.User = await _userHelper.GetUserByEmailAync(this.User.Identity.Name);
                await _roomRepository.CreateAsync(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            var room = await _roomRepository.GetByIdAsync(id.Value);

            if (room == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.Id)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                    //room.User = await _userHelper.GetUserByEmailAync(this.User.Identity.Name);
                    await _roomRepository.UpdateAsync(room);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _roomRepository.ExistAsync(room.Id))
                    {
                        return new NotFoundViewResult("RoomNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            var room = await _roomRepository.GetByIdAsync(id.Value);
            if (room == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            await _roomRepository.DeleteAsync(room);
            return RedirectToAction(nameof(Index));
        }

        public User User { get; set; }

        public IActionResult RoomNotFound()
        {
            return View();
        }
    }
}
