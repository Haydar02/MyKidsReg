﻿using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using System.ComponentModel.Design;

namespace MyKidsReg.Repositories
{
    public interface IUserRepository
    {
        Task <List<User>> GetAll();
        Task <User> GetUserById(int userId);
        Task <User> GetUserByUsername(string username);
        Task CreateUser(User newUser);
        Task UpdateUser(User updateUser);
        Task DeleteUser(int userId);
        Task<bool> UserExists(string username, string email, long mobileNumber);
    }
    public class UserRepositories : IUserRepository
    {
        private readonly MyKidsRegContext _context;

        public UserRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User newUser)
        {
            try
            {
                if (await UserExists(newUser.User_Name, newUser.E_mail, newUser.Mobil_nr))
                {
                    throw new Exception("En bruger med det angivne brugernavn, e-mail eller mobilnumme findes allerede.");
                }

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Her kan du logge fejlen eller give en brugervenlig besked
                throw new Exception("Der opstod en fejl under oprettelse af brugeren. Kontakt venligst systemadministratoren.");
            }
        }



        public async Task DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.User_Id == userId);
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Brugeren findes ikke "); 
            }
        }

        public async Task< List< User>> GetAll()
        {
           return _context.Users.ToList();
        }

        public async Task <User?> GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.User_Id == userId);
        }

        public async Task <User?> GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.User_Name == username);
        }

        public async Task UpdateUser(User updateUser)
        {
            _context.Users.Update(updateUser);
            _context.SaveChanges();
        }
        public async Task<bool> UserExists(string username, string email, long mobileNumber)
        {
            try
            {
                if (username != null)
                {
                    return await _context.Users.AnyAsync(u => u.User_Name == username);
                }
                else if (email != null)
                {
                    return await _context.Users.AnyAsync(u => u.E_mail == email);
                }
                else if (mobileNumber != 0)
                {
                    return await _context.Users.AnyAsync(u => u.Mobil_nr == mobileNumber);
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Fejl under kontrol af brugerens eksistens: {ex.Message}");
                throw; 
            }
        }


    }
}
