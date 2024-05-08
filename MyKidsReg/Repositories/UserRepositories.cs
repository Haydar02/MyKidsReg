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
            _context.Users.Add(newUser);
            _context.SaveChanges();
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
    }
}
