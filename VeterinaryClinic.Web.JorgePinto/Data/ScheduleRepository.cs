using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly DataContext _context;

        public ScheduleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        

        
    }
}
