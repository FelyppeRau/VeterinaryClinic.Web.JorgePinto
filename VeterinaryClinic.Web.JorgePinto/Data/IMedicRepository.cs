using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public interface IMedicRepository : IGenericRepository<Medic>
    {
        public IQueryable GetAllWithUsers();

        public Task<Medic> GetByIdMedicAsync(int id);

        IEnumerable<SelectListItem> GetComboMedics();

    }
}
