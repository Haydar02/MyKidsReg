
using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public interface IStudentLogRepositories
    {
        Task<List<StudentLog>> GetAll();
        Task<StudentLog> GetById(int id);
        Task CreateStudentLog(StudentLog studentLog);
        Task UpdateStudentLog(int id, StudentLog studentLog);
        Task DeleteStudentLog(int id);
    }
    public class StudentLogRepositories : IStudentLogRepositories
    {
        private readonly MyKidsRegContext _context;
        public StudentLogRepositories (MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<List<StudentLog>> GetAll()
        {
            return await _context.StudentLogs.ToListAsync();
        }

        public async Task<StudentLog> GetById(int id)
        {

            return await _context.StudentLogs.FindAsync(id);
        }

        public async Task CreateStudentLog(StudentLog studentLog)
        {
            _context.StudentLogs.Add(studentLog);

            _context.SaveChanges();
        }
        public async Task DeleteStudentLog(int id)
        {
            var item = await _context.StudentLogs.FirstOrDefaultAsync(i => i.Student_Id == id);
            if (item != null)
            {
                _context.StudentLogs.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateStudentLog(int id, StudentLog studentLog)
        {
            _context.StudentLogs.Update(studentLog);
            await _context.SaveChangesAsync();
        }
    }
}
