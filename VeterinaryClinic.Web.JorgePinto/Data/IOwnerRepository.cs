using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public interface IOwnerRepository : IGenericRepository<Owner>  
    {
        public IQueryable GetAllWithUsers();


        public Task<Owner> GetByIdOwnerAsync(int id);


        IEnumerable<SelectListItem> GetComboOwners(); // VERIFICAR COMO SOMENTE APARECER NA COMBOX O OWNER DO USUARIO LOGADO
    }
}
