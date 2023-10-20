using Microsoft.EntityFrameworkCore;
using System.Linq;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context) : base(context)
        {
            _context = context;
        }
       
    }
}
