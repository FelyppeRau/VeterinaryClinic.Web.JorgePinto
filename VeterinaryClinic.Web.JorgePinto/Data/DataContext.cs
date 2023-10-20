using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        // (QUALQUER TABELA DEVE SER CIRADA AQUI!!!)
        public DbSet<Medic> Medics { get; set; } // Propriedade ligada a tabela Medic 
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Room> Rooms { get; set; }           
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public DbSet<AppointmentDetailTemp> AppointmentDetailsTemps { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        ////HABILITAR A REGRA DE APAGAR EM CASCATA (CASCADE DELETE RULE) --    Tem a opção de ser Update também        
        ///Devemos reconsttruir o seed (deitar a DB abaixo)
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var cascadeFKs = modelBuilder.Model
        //        .GetEntityTypes()
        //        .SelectMany(t => t.GetForeignKeys())
        //        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        //    foreach (var fk in cascadeFKs)
        //    {
        //        fk.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
