using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Differencing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public ConverterHelper(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public Animal ToAnimal(AnimalViewModel model, Guid imageId, bool isNew)
        {
            return new Animal
            {
                Id = isNew ? 0 : model.Id,
                Name = model.Name,
                Sex = model.Sex,
                Breed = model.Breed,
                Species = model.Species,
                Birthday = model.Birthday,
                //Weight = model.Weight,
                ImageId = imageId,
                User = model.User,
            };
        }

        public AnimalViewModel ToAnimalViewModel(Animal animal)
        {
            return new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Sex = animal.Sex,
                Breed = animal.Breed,
                Species = animal.Species,
                Birthday = animal.Birthday,
                //Weight = animal.Weight,
                ImageId = animal.ImageId,
                User = animal.User,

            };
        }

        public Medic ToMedic(MedicViewModel model, Guid imageId, bool isNew)
        {
            return new Medic
            {
                Id = isNew ? 0 : model.Id,
                OMV = model.OMV,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CellPhone = model.CellPhone,
                Email = model.Email,
                ImageId = imageId,
                User = model.User,
            };
        }

        public MedicViewModel ToMedicViewModel(Medic medic)
        {
            return new MedicViewModel
            {
                Id = medic.Id,
                OMV = medic.OMV,
                FirstName = medic.FirstName,
                LastName = medic.LastName,
                CellPhone = medic.CellPhone,
                Email = medic.Email,
                ImageId = medic.ImageId,
                User = medic.User,

            };
               
        }

        public Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CellPhone = model.CellPhone,
                Email = model.Email,
                Address = model.Address,
                ImageId = imageId,
                User = model.User,
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                Document = owner.Document,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                CellPhone = owner.CellPhone,
                Email = owner.Email,
                Address = owner.Address,
                ImageId = owner.ImageId,
                User = owner.User,

            };
        }

       
        //public User AddUser(string role, string firstName, string lastName, string email, string userName, string cellPhone, string password = "123456")
        //{
        //    var user = new User
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        Email = email,
        //        PhoneNumber = cellPhone,
        //        UserName = email,
        //    }; /*_context.SaveChanges();*/

        //    _context.Users.Add(user);
        //    _userHelper.AddUserAsync(user, password).Wait();
        //    _userHelper.AddUserToRoleAsync(user, role).Wait();
        //    //_context.SaveChanges();

        //    //var result = _context.Users.FirstOrDefault(user);

        //    // Estou gerando o token e já confirmando para este User
        //    var token = _userHelper.GenerateEmailConfirmationTokenAsync(user).Result;
        //    _userHelper.ConfirmEmailAsync(user, token).Wait();

        //    var result = _context.Users
        //        .Where(e => e.UserName == user.UserName)
        //        .AsEnumerable()
        //        .FirstOrDefault();

        //    if (result != null)
        //    {
        //        return result;
        //    }

        //    return _context.Users.FirstOrDefault();
        //}
    }
}
