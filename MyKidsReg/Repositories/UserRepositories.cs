using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using System.ComponentModel.Design;

namespace MyKidsReg.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetUserById(int userId);
        Task<User> GetUserByUsername(string username);
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
                string userTypeText = UserTypeToText(newUser.Usertype);

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
            
            var user = await _context.Users
                .Include(u => u.AdminRelations)
                .Include(u => u.Messages)
                .Include(u => u.ParentsRelations)
                .Include(u => u.TeacherRelations)
                .FirstOrDefaultAsync(u => u.User_Id == userId);

            if (user == null)
            {
                throw new Exception("Brugeren findes ikke");
            }

            
            _context.AdminRelations.RemoveRange(user.AdminRelations);
            _context.Messages.RemoveRange(user.Messages);
            _context.ParentsRelations.RemoveRange(user.ParentsRelations);
            _context.TeacherRelations.RemoveRange(user.TeacherRelations);

          
            _context.Users.Remove(user);

           
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.User_Id == userId);
        }

        public async Task<User?> GetUserByUsername(string username)
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
        public string UserTypeToText(User_type userType)
        {
            switch (userType)
            {
                case User_type.Super_Admin :
                    return "Super Admin";
                case User_type.Admin:
                    return "Admin";
                case User_type.Padagogue:
                    return "Padagogue";
                case User_type.Parent:
                    return "Parent";
                default:
                    throw new ArgumentOutOfRangeException(nameof(userType), userType, "Unsupported user type");
            }

        }
    }
}
