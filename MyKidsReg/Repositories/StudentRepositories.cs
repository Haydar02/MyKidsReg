using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using System;

namespace MyKidsReg.Repositories
{
    public interface IStudentRepositores
    {
        Task <List<Student>> GetAll();
        Task<Student>GetByID(int id);
        Task<Student> CreateAsync(Student newStudent);
        Task UpdateStudent(int id,Student student);
        Task DeleteStudent(int id);
    }
    public class StudentRepositories : IStudentRepositores
    {
        private readonly MyKidsRegContext _context;

        public StudentRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateAsync(Student newStudent)
        {
            try
            {
                if(await StudentExist(newStudent.Name, newStudent.Last_name, newStudent.Birthday))
                {
                    throw new Exception("Barnet er allerede registreret");
                }
                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw ex;
            }
           

            return newStudent;

        }

        public async Task DeleteStudent(int id)
        {
            var student = await _context.Students
                                        .Include(s => s.ParentsRelations)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                throw new Exception($"Barnet med denne ID: {id} ikke findes.");
            }
                      
            
            _context.ParentsRelations.RemoveRange(student.ParentsRelations);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByID(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task UpdateStudent(int id, Student student)
        {
           _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> StudentExist(string Name, String last_name,string birthday)
        {
            try
            {
                if (Name != null)
                {
                    return await _context.Students.AnyAsync(s => s.Name == Name);
                }
                else if (last_name != null) 
                {
                    return await _context.Students.AnyAsync(l => l.Last_name == last_name);
                }
                else if (birthday != null)
                {
                    return await _context.Students.AnyAsync(d => d.Birthday == birthday);
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
