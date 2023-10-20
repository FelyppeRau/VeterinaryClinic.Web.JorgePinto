using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class MedicRepository : GenericRepository<Medic>, IMedicRepository
    {
        private readonly DataContext _context;

        public MedicRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Medics.Include(p => p.User);
        }

        public async Task<Medic> GetByIdMedicAsync(int id)
        {
            return await _context.Set<Medic>().FirstOrDefaultAsync(e => e.Id == id);
        }

        

        public IEnumerable<SelectListItem> GetComboMedics()
        {
            var list = _context.Medics.OrderBy(a => a.FirstName).Select(a => new SelectListItem
            {
                Text = a.FullName,
                Value = a.Id.ToString(),                

            }).ToList();

            list.Insert(0, new SelectListItem // Assim na combo box não vem o primeiro item dos Medicos, vem o text abaixo:
            {
                Text = "(Select a Medic...)",
                Value = "0"
            });

            return list;
        }
    }
}
 