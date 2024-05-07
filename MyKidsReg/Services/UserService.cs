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
        private readonly CommunicationService _communication;

        public UserService(IUserRepository rep, PasswordService passwordService, CommunicationService communication)
        {
            _rep = rep;
            _passwordService = passwordService;
            _communication = communication;
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
            try
            {
                _rep.CreateUser(newUser);
                _communication.SendTemoraryPassword(username, temporaryPassword, "email");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating user: {ex.Message}");
            }
        }
        private string GenerateTemporaryPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
       
        //public void SendEmail(string userEmail, string subject, string body)
        //{
        //    Console.WriteLine($"E_mail sendt til {userEmail} med emne {subject} og inhold : {body}");
        //}
        //public void SendSMS(string mobil_Nr, string message)
        //{
        //    Console.WriteLine($" SMS sendt til {mobil_Nr} med besked : {message}");
        //}

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