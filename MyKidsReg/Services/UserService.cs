using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MyKidsReg.Models;
using MyKidsReg.Repositories;
using System.Numerics;

namespace MyKidsReg.Services
{
    public interface IUserService
    {
        void createUserWithTemporaryPAssword(string username, string password, string email, int Mobil_Nr);
        void CreaateUser(string username, string password,string name, string last_name, string adress,int zip_code,string E_mail,long mobilNumber, User_type user_Type);
        User GetUserByID(int id);
        User GetUserByName(string username);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _rep;
        private readonly PasswordService _passwordService;

        public UserService(IUserRepository rep, PasswordService passwordService)
        {
            _rep = rep;
            _passwordService = passwordService;
        }

        public void createUserWithTemporaryPAssword(string username, string password, string email, int Mobil_Nr)
        {
            string temporaryPassword = GenerateTemporaryPassword();
            string passwordHash = _passwordService.HashPassword(temporaryPassword);

            var newUser = new User
            {
                User_Name = username,
                Password = passwordHash,
                E_mail = email,
                Mobil_nr = Mobil_Nr
            };
            _rep.CreateUser(newUser);
            SendTemoraryPassword(username, temporaryPassword,"email");

        }
        private string GenerateTemporaryPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
        private void SendTemoraryPassword(string username, string temporaryPassword, string deliveryMethod)
        {
            var user = _rep.GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception("Brugeren findes ikke ");

            }
            switch (deliveryMethod.ToLower())
            {
                case "email":
                    SendEmail(user.E_mail, "MidlerTidigt Email", $"Din midlertidige Adgangskode er :{temporaryPassword}");
                    break;
                case "sms":
                    if(user.Mobil_nr != 0)
                    {
                        string mobil_number = user.Mobil_nr.ToString();
                        SendSMS(mobil_number, $"Din midlertidigt adgangskode er :{temporaryPassword}");
                    }
                    else
                    {
                        throw new Exception("Mobilnummeret er ikke tilgængeligt");
                    }
                   
                    break;
                default:
                    Console.WriteLine("Ugyldig leveringsmetod.");
                    break;
            }

        }
        public void SendEmail(string userEmail, string subject, string body)
        {
            Console.WriteLine($"E_mail sendt til {userEmail} med emne {subject} og inhold : {body}");
        }
        public void SendSMS(string mobil_Nr, string message)
        {
            Console.WriteLine($" SMS sendt til {mobil_Nr} med besked : {message}");
        }

        public void CreaateUser(string username, string password, string name, string last_name, string adress, int zip_code,string E_mail, long mobilNumber, User_type user_Type)
        {
           string passwordHash = _passwordService.HashPassword(password);
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
            _rep.CreateUser(newUser);
        }

        public User GetUserByID(int id)
        {
            return _rep.GetUserById(id);
        }

        public User GetUserByName(string username)
        {
            return _rep.GetUserByUsername(username);
        }

        public void UpdateUser(int id, User update)
        {
            var existingUser = _rep.GetUserById(id);
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

        public void DeleteUser(int id)
        {
            _rep.DeleteUser(id);
        }
    }
}