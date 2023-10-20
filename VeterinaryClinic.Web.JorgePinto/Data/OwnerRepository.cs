using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Owners.Include(p => p.User);
        }


        public async Task<Owner> GetByIdOwnerAsync(int id)
        {
            return await _context.Set<Owner>().FirstOrDefaultAsync(e => e.Id == id);
        }


        //TODO: VERIFICAR COMO SOMENTE APARECER NA COMBOX IGUAL AO USER LOGADO CASO NÃO SEJA O MEDICO (regra: se conter "xpto" o email então aparece toda a lista)
        public IEnumerable<SelectListItem> GetComboOwners()  // VERIFICAR COMO SOMENTE APARECER NA COMBOX IGUAL AO USER LOGADO CASO NÃO SEJA O MEDICO
        {
            var list = _context.Owners.OrderBy(a => a.FirstName).Select(a => new SelectListItem
            {
                Text = a.FullName,
                Value = a.Id.ToString(),

            }).ToList();

            list.Insert(0, new SelectListItem // Assim na combo box não vem o primeiro item dos Owners, vem o text abaixo:
            {
                Text = "(Select a Owner...)",
                Value = "0"
            });

            return list;
        }
    }
}
