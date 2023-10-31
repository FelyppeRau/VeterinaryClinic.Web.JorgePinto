using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _ramdom;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper; 
            _ramdom = new Random();
        }

        public async Task SeedAsync()
        {
            //await _context.Database.EnsureCreatedAsync(); // Não cria os Migrations
            await _context.Database.MigrateAsync(); // Cria os Migrations ao correr o Seed

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Medic");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Anonymous");

            //AddAdmin(); // Chamando o Admin 

            var user = await _userHelper.GetUserByEmailAsync("felypperau@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Jorge",
                    LastName = "Pinto",
                    Email = "felypperau@gmail.com",
                    UserName = "felypperau@gmail.com",
                    PhoneNumber = "123456789",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!_context.Medics.Any())
            {
                AddMedic("Cintia", "Rau", user);
                AddMedic("Laura", "Rau", user);
                AddMedic("Cecilia", "Rau", user);
                AddMedic("Stella", "Neves", user);

                await _context.SaveChangesAsync();
            }

            if (!_context.Owners.Any())
            {
                AddOwner("Mariana", "Leite", "Praceta FlorBela", user);
                AddOwner("Filipe", "Baptista", "Av. Portugal", user);
                AddOwner("Maria", "Silva", "Rua Thomás Ribeiro", user);
                AddOwner("Bruno", "Ferreira", "Av. do Forte", user);
                AddOwner("Simao", "Andre", "Estrada Outurela", user);
                AddOwner("Luis", "Patricio", "Rua Manoel Teixeira", user);
                AddOwner("Dario", "Dias", "Rua Monte Flor", user);
                AddOwner("Laercio", "Santos", "Rua Jose Viana", user);
                AddOwner("Tatiane", "Avellar", "Rua Adolfo Casais", user);
                AddOwner("Claudia", "Bird", "Rua Antonio Pedro", user);
                AddOwner("Nuno", "Santos", "Rua Henrique Mendes", user);
                AddOwner("Catarina", "Palma", "Rua Maria Barroso", user);
                AddOwner("Diogo", "Alves", "Rua Tomás Vieira", user);
                AddOwner("Mariana", "Oliveira", "Rua Almeida Garrett", user);
                AddOwner("Reinaldo", "Souza", "Estrada Nova", user);
                AddOwner("Ruben", "Correia", "Av. dos Cavaleiros", user);
                AddOwner("Duarte", "Marques", "Rua Antonio Gomes", user);
                AddOwner("Licinio", "Rosario", "Rua Alberto Osório", user);
                AddOwner("Joel", "Rangel", "Rua Rui Andrade", user);
                AddOwner("Pedro", "Silva", "Rua Fernando Távora", user);
                AddOwner("Luis", "Leopoldo", "Av. Vitor Figueiredo", user);
                AddOwner("Ana", "Ribeiro", "Rua Eça de Queiroz", user);


                await _context.SaveChangesAsync();
            }

            if (!_context.Animals.Any())
            {

                AddAnimal("Mike", "M", "Beagle","Canine", user);
                AddAnimal("Laila", "F", "Border Collie", "Canine", user);
                AddAnimal("Luluca", "F", "Pug", "Canine", user);
                AddAnimal("Toy", "M", "Schnauzer", "Canine", user);
                AddAnimal("Kitana", "F", "Siberian Husky", "Canine", user);
                AddAnimal("Zac", "M", "Chow Chow", "Canine", user);
                AddAnimal("Stallone", "M", "Lizard", "Reptile", user);
                AddAnimal("Tigger", "M", "German Shepherd", "Canine", user);
                AddAnimal("Mag", "F", "Macaw", "Bird", user);
                AddAnimal("Luca", "M", "Parrot", "Bird", user);
                AddAnimal("Anitta", "F", "British Shorthais", "Feline", user);
                AddAnimal("Mia", "F", "Siamese", "Feline", user);
                AddAnimal("Juca", "M", "Border Collie", "Canine", user);
                AddAnimal("Max", "M", "Bernese Mountain", "Canine", user);

                await _context.SaveChangesAsync();
            }
        }

        private void AddAdmin()
        {
            var user = _userHelper.GetUserByEmailAsync("felypperau@gmail.com");

            //if (user == null) // Se não existir User, cria!
            //{
            //    var addUser = AddUser("Admin", "Jorge", "Pinto", "", "felypperau@gmail.com", "934567890", "123456");
            //}
        }

        private void AddMedic(string firstName, string lastName, User user)
        {
            var cellPhone = "9" + _ramdom.Next(99999999).ToString();
            var email = firstName.ToLower() + lastName.ToLower() + "@vetclinic.com";

            var addUser = AddUser("Medic", firstName, lastName, email, email, cellPhone, "123456").Result;

            // Task<User> AddUser(string role, string firstName, string lastName, string email, string userName, string phoneNumber, string password = "123456")

            _context.Medics.Add(new Medic
            {
                OMV = _ramdom.Next(9999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                CellPhone = cellPhone,
                Email = email,
                User = addUser,
            }); ;
        }

        private void AddOwner(string firstName, string lastName, string address, User user)
        {
            var cellPhone = "9" + _ramdom.Next(99999999).ToString();
            var email = firstName.ToLower() + lastName.ToLower() + "@yopmail.com";

            var addUser = AddUser("Owner", firstName, lastName, email, email, cellPhone, "123456").Result;

            _context.Owners.Add(new Owner
            {
                Document = _ramdom.Next(9999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                CellPhone = cellPhone,
                Address = address,
                Email = email,
                User = addUser,
            });
        }

        private void AddAnimal(string name, string sex, string breed, string species, User user)
        {
            Random random = new Random();

            // Defina as datas de início e fim do intervalo desejado
            DateTime startDate = new DateTime(2015, 1, 1);
            DateTime endDate = new DateTime(2023, 10, 20);

            // Gere uma data aleatória entre as datas de início e fim
            TimeSpan range = endDate - startDate;
            int rangeDays = random.Next((int)range.TotalDays);
            DateTime birthday = startDate.AddDays(rangeDays);

            _context.Animals.Add(new Animal
            {
                Name = name,
                Sex = sex,
                Breed = breed,
                Species = species,  
                Birthday = birthday,
                //ImageUrl
                User = user,
            });
        }

        public async Task<User> AddUser(string role, string firstName, string lastName, string email, string userName, string phoneNumber, string password = "123456")
        {
            var user = await _userHelper.GetUserByEmailAsync(email);

            if (user != null)
            {
                return user;
            }

            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                PhoneNumber = phoneNumber,                
            };

            var result = await _userHelper.AddUserAsync(user, password);

            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            // Adiciona a Role "Medic" aos usuários com domínio "@vetclinic.com"  -  CORRIGIR QNDO TIVER OS DOMINIOS
            if (email.Contains(".vetclinic@yopmail.com"/*"@vetclinic.com"*/))
            {
                await _userHelper.AddUserToRoleAsync(user, "Medic");
            }

            //// Adiciona a Role "Owner" aos usuários com quaisquer domínio
            //else
            //{
            //    await _userHelper.AddUserToRoleAsync(user, "Owner");
            //}

            if (role != null)
            {
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

            await _userHelper.ConfirmEmailAsync(user, token);

            if (role != null)
            {
                var isInRole = await _userHelper.IsUserInRoleAsync(user, role);

                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, role);
                }
            }

            user = await _userHelper.GetUserByEmailAsync(user.Email);

            return user;
        }
        //public async Task <User> AddUser(string role, string firstName, string lastName, string email, string userName, string cellPhone, string password = "123456")
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

        //    return _context.Users.FirstOrDefault(i => i.Email == user.Email);
        //}
    }
}
