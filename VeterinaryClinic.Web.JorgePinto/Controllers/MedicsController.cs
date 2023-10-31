using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class MedicsController : Controller
    {
        private readonly IMedicRepository _medicRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public MedicsController(IMedicRepository medicRepository,
                                IUserHelper userHelper,
                                IBlobHelper blobHelper,
                                IConverterHelper converterHelper)
        {
            _medicRepository = medicRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        } 

        // GET: Medics       
        public IActionResult Index()
        {
            return View(_medicRepository.GetAll().OrderBy(p => p.FirstName));
        }

        // GET: Medics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MedicNotFound");
            }

            var medic = await _medicRepository.GetByIdAsync(id.Value);

            if (medic == null)
            {
                return new NotFoundViewResult("MedicNotFound");
            }

            return View(medic);
        }

        // GET: Medics/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicViewModel model)
        {
            if (ModelState.IsValid)
            {
                // *** INICIO PARA GRAVAR A IMAGEM ***
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "medics");
                }

                // *** FIM PARA GRAVAR A IMAGEM ***
                var medic = _converterHelper.ToMedic(model, imageId, true);

                //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                model.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                await _medicRepository.CreateAsync(medic);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Medics/Edit/5
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MedicNotFound");
            }

            var medic = await _medicRepository.GetByIdMedicAsync(id.Value);

            if (medic == null)
            {
                return new NotFoundViewResult("MedicNotFound");
            }


            var model = _converterHelper.ToMedicViewModel(medic);

            return View(model);
        }

        // POST: Medics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "medics");

                        model.ImageId = imageId;
                    }

                    await _medicRepository.UpdateAsync(model);

                    //var medic = _converterHelper.ToMedic(model, imageId, false);

                    ////TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                    //medic.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    //await _medicRepository.UpdateAsync(medic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _medicRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("MedicNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Medics/Delete/5       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MedicNotFound");
            }

            var medic = await _medicRepository.GetByIdAsync(id.Value);

            if (medic == null)
            {
                return new NotFoundViewResult("MedicNotFound");
            }

            return View(medic);
        }

        // POST: Medics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medic = await _medicRepository.GetByIdAsync(id);

            try
            {
                await _medicRepository.DeleteAsync(medic);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"The {medic.FullName} is probably linked to some schedule!";
                    ViewBag.ErrorMessage = $"The {medic.FullName} cannot be deleted, as there are appointments linked.</br></br>" +
                         $"First, try deleting all linked appointments and after deleting the Medic again.";
                }
            }

            return View("Error");
        }

        public IActionResult MedicNotFound()
        {
            return View();
        }
    }
}
