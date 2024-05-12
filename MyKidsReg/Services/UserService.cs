using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MyKidsReg.Models;
using MyKidsReg.Repositories;
using MyKidsReg.Services.CommunicationsServices;
using System.Numerics;

namespace MyKidsReg.Services
{
     
        public interface IUserService
        {
        Task createUserWithTemporaryPassword(string username, string name, string last_name,
                                            string adress, int zip_code, string E_mail,
                                            long mobilNumber, User_type user_Type);

        Task CreaateUser(string username, string name, string last_name, string adress, int zip_code, string E_mail, long mobilNumber, User_type user_Type);
            Task<User> GetUserByID(int id);
            Task<List<User>> GetAlle();
            Task<User> GetUserByName(string username);
            Task UpdateUser(int id, User user);
            Task DeleteUser(int id);
           
        }
        public class UserService : IUserService
        {
            private readonly IUserRepository _rep;
            private readonly PasswordService _passwordService;
            private readonly CommunicationService _communication;
      

        public UserService(IUserRepository rep, PasswordService passwordService, CommunicationService communication)
            {
                _rep = rep;
                _passwordService = passwordService;
                _communication = communication;
            }

        public async Task createUserWithTemporaryPassword(string username, string name, string last_name,
                                                     string adress, int zip_code, string E_mail,
                                                     long mobilNumber, User_type user_Type)

        {
            string temporaryPassword = GenerateTemporaryPassword();
                string passwordHash = _passwordService.HashPassword(temporaryPassword);

                var newUser = new User
                {
                    User_Name = username,
                    Password = passwordHash,
                    Name = name,
                    Last_name = last_name,
                    Address = adress,
                    Zip_code = zip_code,
                    E_mail = E_mail,
                    Mobil_nr = mobilNumber,
                    Usertype = user_Type
                };
            

            DateTime expirationTime = DateTime.Now.AddHours(5);
            newUser.TemporaryPasswordExpiration = expirationTime;

            try
            {
                await _rep.CreateUser(newUser);
                await _communication.SendTemporaryPassword(username, temporaryPassword, "email");

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating user: {ex.Message}");
            }
        }

        public async Task<bool> PerformActionRequiringTemporaryPasswordExpirationCheck(int userId)
        {
            // Hent brugeren fra databasen
            User user = await _rep.GetUserById(userId);

            // Kontroller om den midlertidige adgangskode er udløbet
            if (user != null && user.IsTemporaryPasswordExpired())
            {
                // Håndter logikken for en udløbet midlertidig adgangskode
                // For eksempel: Bloker brugeren eller bed dem om at nulstille adgangskoden

                // Generer en ny midlertidig adgangskode
                string newTemporaryPassword = GenerateTemporaryPassword();

                // Opdater den midlertidige adgangskode i brugerens objekt
                user.Password = _passwordService.HashPassword(newTemporaryPassword);

                // Indstil den nye udløbstid for den midlertidige adgangskode til 5 timer fra nu
                user.TemporaryPasswordExpiration = DateTime.Now.AddHours(5);

                // Gem ændringerne i databasen
                await _rep.UpdateUser(user);

                // Send den nye midlertidige adgangskode til brugeren (f.eks. via e-mail eller SMS)
                await _communication.SendTemporaryPassword(user.User_Name, newTemporaryPassword, "email");

                // Fortsæt med at udføre handlingen, der kræver en gyldig midlertidig adgangskode
                // ...

                return true;
            }

            // Fortsæt med at udføre handlingen, der kræver en gyldig midlertidig adgangskode
            // ...

            return true;
        }


        private string GenerateTemporaryPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
       

        public async Task CreaateUser(string username, string name, string last_name, string adress, int zip_code,string E_mail, long mobilNumber, User_type user_Type)
        {
           
            string userTypeString = user_Type.ToText();
            var newUser = new User
            {
                User_Name = username,               
                Name = name,
                Last_name = last_name,
                Address = adress,
                Zip_code = zip_code,
                E_mail = E_mail,
                Mobil_nr = mobilNumber,
                Usertype = user_Type

            };
            newUser.UserValidate();
            string userTypeText = newUser.Usertype.ToText();
            Console.WriteLine($"User type: {userTypeText}");
            newUser.UsernameValidate();
           await _rep.CreateUser(newUser);
        }

        public async Task< User> GetUserByID(int id)
        {
            return await _rep.GetUserById(id);
        }

        public async Task<User> GetUserByName(string username)
        {
            return await _rep.GetUserByUsername(username);
        }

        public async Task UpdateUser(int id, User update)
        {
            var existingUser = await _rep.GetUserById(id);
            if (existingUser != null)
            {
              existingUser.User_Name = update.User_Name;
                existingUser.Password = update.Password;
                existingUser.Name = update.Name;
                existingUser.Last_name = update.Last_name;
                existingUser.Address = update.Address;
                existingUser.Zip_code = update.Zip_code;
                existingUser.E_mail = update.E_mail;
                existingUser.Mobil_nr = update.Mobil_nr;
                existingUser.Usertype = update.Usertype;

                _rep.UpdateUser(existingUser);
            }
        }

        public async Task DeleteUser(int id)
        {
            _rep.DeleteUser(id);
        }

        public async Task < List< User>> GetAlle()
        {
            return  await _rep.GetAll();
        }
    }
}