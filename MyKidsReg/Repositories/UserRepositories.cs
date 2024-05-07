using MyKidsReg.Models;
using System.ComponentModel.Design;

namespace MyKidsReg.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        void CreateUser(User newUser);
        void UpdateUser(User updateUser);
        void DeleteUser(int userId);
    }
    public class UserRepositories : IUserRepository
    {
        private readonly MyKidsRegContext _context;

        public UserRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public void CreateUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
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

        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.User_Id == userId);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.User_Name == username);
        }

        public void UpdateUser(User updateUser)
        {
            _context.Users.Update(updateUser);
            _context.SaveChanges();
        }
    }
}
