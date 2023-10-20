using System;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Helpers
{
    public interface IConverterHelper
    {

        Animal ToAnimal(AnimalViewModel model, Guid imageId, bool isNew);

        AnimalViewModel ToAnimalViewModel(Animal animal);

        Medic ToMedic(MedicViewModel model, Guid imageId, bool isNew);

        MedicViewModel ToMedicViewModel(Medic mediic);

        Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew);

        OwnerViewModel ToOwnerViewModel(Owner owner);

        //User AddUser(string role, string firstName, string lastName, string email, string userName, string cellPhone, string password = "123456");
    }
}
