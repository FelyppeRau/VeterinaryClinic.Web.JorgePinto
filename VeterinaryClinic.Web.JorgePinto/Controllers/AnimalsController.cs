using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Helpers;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{

   
    public class AnimalsController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public AnimalsController(IAnimalRepository animalRepository,
                                 IUserHelper userHelper,
                                 IBlobHelper blobHelper,
                                 IConverterHelper converterHelper)
        {
            _animalRepository = animalRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Animals
        //[Authorize(Roles = "Medic, Customer, Admin")] //**************RETIRAR O ADMIN********************
        public IActionResult Index()
        {
            return View(_animalRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Animals/Details/5
        //[Authorize(Roles = "Medic")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AnimalNotFound");
            }

            var animal = await _animalRepository.GetByIdAsync(id.Value);

            if (animal == null)
            {
                return new NotFoundViewResult("AnimalNotFound");
            }

            return View(animal);
        }

        // GET: Animals/Create
        //[Authorize(Roles = "Medic")] //**************RETIRAR O ADMIN********************
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                // *** INICIO PARA GRAVAR A IMAGEM ***
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "animals");
                }

                // *** FIM PARA GRAVAR A IMAGEM ***
                var animal = _converterHelper.ToAnimal(model, imageId, true);

                //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                model.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                await _animalRepository.CreateAsync(animal);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        // GET: Animals/Edit/5
       // [Authorize(Roles = "Medic, Customer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AnimalNotFound");
            }

            var animal = await _animalRepository.GetByIdAnimalAsync(id.Value);

            if (animal == null)
            {
                return new NotFoundViewResult("AnimalNotFound");
            }

            var model = _converterHelper.ToAnimalViewModel(animal);

            return View(model);
        }


        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO 
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "animals");

                        model.ImageId = imageId;
                    }

                    await _animalRepository.UpdateAsync(model);

                    //var animal = _converterHelper.ToAnimal(model, imageId, false);
                    
                    //animal.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    //await _animalRepository.UpdateAsync(animal);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _animalRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("AnimalNotFound");
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

        // GET: Animals/Delete/5
       // [Authorize(Roles = "Medic")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AnimalNotFound");
            }

            var animal = await _animalRepository.GetByIdAsync(id.Value);

            if (animal == null)
            {
                return new NotFoundViewResult("AnimalNotFound");
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);

            try
            {    
                //throw new Exception("Exceção de Teste");
                await _animalRepository.DeleteAsync(animal);
                return RedirectToAction(nameof(Index));
            }   
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"The {animal.Name} is probably linked to some schedule!";
                    ViewBag.ErrorMessage = $"The {animal.Name} cannot be deleted, as there are appointments linked.</br></br>" +
                        $"First, try deleting all linked appointments and after deleting the Animal again.";
                }
            }

            return View("Error");            
        }

        public IActionResult AnimalNotFound()
        {
            return View();
        }
    }
}
