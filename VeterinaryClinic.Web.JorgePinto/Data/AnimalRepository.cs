using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class AnimalRepository : GenericRepository<Animal>, IAnimalRepository
    {
        private readonly DataContext _context;

        public AnimalRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Animals.Include(p => p.User);
        }

        public async Task<Animal> GetByIdAnimalAsync(int id)
        {
            return await _context.Set<Animal>().Include(p => p.Owner).FirstOrDefaultAsync(e => e.Id == id);
        }


        //TODO: VERIFICAR COMO SOMENTE APARECER NA COMBOX OS ANIMAIS DO USUARIO LOGADO (regra: se conter "xpto" o email então aparece toda a lista)
        public IEnumerable<SelectListItem> GetComboAnimals()   // VERIFICAR COMO SOMENTE APARECER NA COMBOX OS ANIMAIS DO USUARIO LOGADO
        {
             
            var list = _context.Animals.OrderBy(a => a.Name).Select( a => new SelectListItem
            {
                Text = a.FullAnimal,
                Value = a.Id.ToString(),                
               
            }).ToList();

            list.Insert(0, new SelectListItem // Na combo box não vem o primeiro item dos Animais, vem o text abaixo:
            {
                Text = "(Select a Animal...)",
                Value = "0"
            });

            return list;
        }
    }
}
