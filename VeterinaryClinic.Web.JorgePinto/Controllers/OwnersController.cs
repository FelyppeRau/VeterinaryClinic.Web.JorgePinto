using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Helpers;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{

    //[Authorize(Roles = "Medic")]
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public OwnersController(IOwnerRepository ownerRepository,
                                 IUserHelper userHelper,
                                 IBlobHelper blobHelper,
                                 IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll().OrderBy(p => p.FirstName));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                // *** INICIO PARA GRAVAR A IMAGEM ***
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                }

                // *** FIM PARA GRAVAR A IMAGEM ***
                var owner = _converterHelper.ToOwner(model, imageId, true);
                               
                model.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); //addUser.Result.Email);

                await _ownerRepository.CreateAsync(owner);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _ownerRepository.GetByIdOwnerAsync(id.Value);

            if (owner == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }


            var model = _converterHelper.ToOwnerViewModel(owner);

            return View(model);
        }


        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");

                        model.ImageId = imageId;
                    }

                    await _ownerRepository.UpdateAsync(model);

                    //var owner = _converterHelper.ToOwner(model, imageId, false);

                    ////TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                    //owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    //await _ownerRepository.UpdateAsync(owner);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ownerRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("OwnerNotFound");
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

        // GET: Owners/Delete/5       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return new NotFoundViewResult("OwnerNotFound");
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Exceção de Teste");
                await _ownerRepository.DeleteAsync(owner);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"The {owner.FullName} is probably linked to some schedule!";
                    ViewBag.ErrorMessage = $"The {owner.FullName} cannot be deleted, as there are appointments linked.</br></br>" +
                         $"First, try deleting all linked appointments and after deleting the Owner again.";
                }
            }

            return View("Error");
        }

        public IActionResult OwnerNotFound()
        {
            return View();
        }
    }
}
