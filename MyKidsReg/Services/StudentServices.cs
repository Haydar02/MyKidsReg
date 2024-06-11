using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IStudentServices
    {
        Task<List<Student>> GetAll();
        Task<Student> GetByID(int id);
        Task<IEnumerable<Student>> GetByDepatmentId(int departmentId);
        Task<Student> CreateStudentAsync(Student newStudent);
        Task UpdateStudents(int id, Student student);
        Task DeleteStudent(int id);
    }
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepositores _rep;

        public StudentServices(IStudentRepositores rep)
        {
            _rep = rep;
        }

        public async Task<Student> CreateStudentAsync(Student newStudent)
        {
          return await _rep.CreateAsync(newStudent);            
            
        }


        public async Task DeleteStudent(int id)
        {
            
           await _rep.DeleteStudent(id);
        }

        public async Task<List<Student>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<Student> GetByID(int id)
        {
            return _rep.GetByID(id);
        }
        public async Task<IEnumerable<Student>> GetByDepatmentId(int departmentId)
        {
            return await _rep.GetByDepartmentId(departmentId);
        }

        public async Task UpdateStudents(int id, Student update)
        {
            var existingStudent = await _rep.GetByID(id);
            if (existingStudent == null)
            {
                throw new InvalidOperationException($"Barnet med denne {id} findes ikke !!!");

            }
            existingStudent.Name = update.Name;    
            existingStudent.Last_name = update.Last_name;
            existingStudent.Birthday = update.Birthday;
            existingStudent.StudentValidate();
            existingStudent.Department_id = update.Department_id;
            await _rep.UpdateStudent(id,existingStudent);

            
        }
    }
}
