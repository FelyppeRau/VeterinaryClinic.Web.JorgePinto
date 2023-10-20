using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public interface IAnimalRepository : IGenericRepository<Animal>
    {
        public IQueryable GetAllWithUsers();

        Task<Animal> GetByIdAnimalAsync(int id);

        IEnumerable<SelectListItem> GetComboAnimals(); // VERIFICAR COMO SOMENTE APARECER NA COMBOX OS ANIMAIS DO USUARIO LOGADO
    }
}
