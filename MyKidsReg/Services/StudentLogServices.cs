using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IStudentLogServices
    {
        Task<List<StudentLog>> GetAll();
        Task<StudentLog> GetById(int id);

        Task CreateStudentLog(StudentLog studentLog);
        Task UpdateStudentLog(int id, StudentLog studentLog);
        Task DeleteStudentLog(int id);
    }
    public class StudentLogServices : IStudentLogServices
    {
        private readonly IStudentLogRepositories _rep;

        public StudentLogServices(IStudentLogRepositories rep)
        {
            _rep = rep;
        }
        public async Task CreateStudentLog(StudentLog studentLog)
        {
            await _rep.CreateStudentLog(studentLog);
        }

        public async Task DeleteStudentLog(int id)
        {
            await _rep.DeleteStudentLog(id);
        }

        public async Task<List<StudentLog>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<StudentLog> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateStudentLog(int id, StudentLog update)
        {
            var existingStudentLog = await _rep.GetById(id);
            if (existingStudentLog != null)
            {
                throw new InvalidOperationException($"StudentLog findes allerede med dette {id}!!!");
            }

            existingStudentLog.Student = update.Student;
            existingStudentLog.Type = update.Type;

            await _rep.UpdateStudentLog(id, existingStudentLog);
        }
    }
}

